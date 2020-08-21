using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{

    public abstract class RayObject
    {

        public enum Axis
        {
            X,
            Y,
            Z
        }

        protected static int currentId = 0;
        protected int id;
        public Material material;
        public bool canReceiveShadows = true;
        public bool canCastShadows = true;

        protected Mat4 matrix = new Mat4();

        protected RayObject parent = null;
        protected List<RayObject> children = new List<RayObject>();

        public Bounds precalculatedBounds = null;

        public RayObject GetParent()
        {
            return parent;
        }

        public bool Includes(RayObject o)
        {
            //Are we the object?

            if(this == o)
            {
                return true;
            }

            //What about our children?
            foreach(RayObject i in GetChildren())
            {
                if (i.Includes(o))
                    return true;
            }

            //Object never found
            return false;
        }

        public List<RayObject> GetChildren()
        {
            return children;
        }


        //Method used outside of class to control setting the parent of an object.
        //We do not directly remove objects, instead we would set an object's parent to null.
        public void SetParent(RayObject newParent)
        {
            //Remove us from any objects that currently have us as a child
            if(parent != null)
            {
                parent.RemoveChild(this);
            }

            //Set ourselves as a child of the parent
            if (newParent != null)
            {
                newParent.AddChild(this);
            }

            //Can't let objects just sit around, make ourselves
            //part of the root
            else if(Scene.current.root != null)
            {
                Scene.current.root.AddChild(this);
            }
            //ONly thing that should get here is the global root of the scene...
            else
            {
                Console.WriteLine("Root group of scene successfully created.");
            }
            
        }

        protected void AddChild(RayObject newChild)
        {
            //Is this already a child of this object?
            if (newChild.parent != this)
            {
                children.Add(newChild);
            }
            newChild.parent = this;
        }

        protected void RemoveChild(RayObject child)
        {
            if (child.parent == this)
            {
                children.Remove(child);
            }
            child.parent = null;
        }

        public void SetMatrix(Mat4 matrix)
        {
            this.matrix = matrix;
        }

        public Mat4 GetMatrix()
        {
            return this.matrix;
        }

        public void SetPosition(Point point)
        {
            this.matrix[0, 3] = point.x;
            this.matrix[1, 3] = point.y;
            this.matrix[2, 3] = point.z;

        }

        public Point GetPosition()
        {
            return new Point(this.matrix[0,3],
                            this.matrix[1, 3],
                            this.matrix[2, 3]);
        }

        virtual protected Ray RayToObjectSpace(Ray ray)
        {
            return GetMatrix().Inverse() * ray;
        }

        public int Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
            }
        }

        public RayObject()
        {
            if (Scene.current != null)
            {
                this.SetParent(Scene.current.root);
                Scene.current.AddRayObject(this);
            }

            id = currentId++;
            material = new Material();
        }

        public abstract Vector CalculateLocalNormal(Point localPoint, Intersection i = null);

        public Vector GetNormal(Point worldPoint, Intersection i = null)
        {
            Point localPoint = this.WorldToObject(worldPoint);
            Vector localNormal = CalculateLocalNormal(localPoint, i);
            Vector worldNormal = NormalToWorld(localNormal);
            return worldNormal;
        }

        public override string ToString()
        {
            return "RayObject: " + id.ToString();
        }

        public abstract List<Intersection> Intersect(Ray ray);

        public Color Lighting(Point position, Light light, Vector eye, Vector normal, bool inShadow = false)
        {
            Color temp = material.color;
            if(material.pattern != null)
            {
                temp = material.pattern.PatternAtObject(this, position);
            }

            Color effectiveColor = temp * light.intensity;
            Vector lightVec = (light.position - position).Normalize();
            Color ambientColor = temp * material.Ambient;
            Color diffuseColor;
            Color specularColor;

            if (inShadow)
                return ambientColor;

            double lDotN = Vector.Dot(lightVec, normal);
            if (lDotN <= 0)
            {
                diffuseColor = Color.black;
                specularColor = Color.black;
            }

            else
            {
                diffuseColor = effectiveColor * material.Diffuse * lDotN;
                Vector reflect = Vector.Reflect(-lightVec, normal);
                double rDotE = Vector.Dot(reflect, eye);

                if (rDotE <= 0)
                    specularColor = Color.black;
                else
                {
                    double factor = (double)Math.Pow((double)rDotE, (double)material.Shinniness);
                    specularColor = light.intensity * material.Specular * factor;
                }
            }

            return ambientColor + diffuseColor + specularColor;
        }

        public Point WorldToObject(Point p)
        {
            //Travels through all parent objects till we hit the root object
            //Then begins returning the point multiplied by the top node matrix
            //down to the bottom
            if(this.GetParent() != null)
            {
                p = this.GetParent().WorldToObject(p);
            }

            return this.GetMatrix().Inverse() * p;
        }

        public Vector NormalToWorld(Vector n)
        {
            n = this.GetMatrix().Inverse().Transpose() * n;
            n.w = 0;
            n.Normalize();

            if(this.GetParent() != null)
            {
                n = this.GetParent().NormalToWorld(n);
            }

            return n;

        }

        public abstract Bounds GetLocalBounds();


        public Bounds GetBounds()
        {
            if(precalculatedBounds == null)
            {
                return CalcBounds();
            }
            return precalculatedBounds;
        }

        public Bounds CalcBounds()
        {
            Bounds finalBound = new Bounds();

            //returns a bounding box for all elements within this object in its space

            List<Bounds> bounds = new List<Bounds>();

            //Get our bounds first.
            finalBound = GetLocalBounds();      //Get the local bounds of the object
            finalBound = finalBound.GetAABB(GetMatrix()); //Convert transformed to axis aligned

            //Look at our children and see if they exceed our geometric boundaries
            //and if so, then extend our current bounds to include them.
            for(int i = 0; i < children.Count; i++)
            {
                //Get the bounds of the child with its transforms taken into consideration
                Bounds bound = children[i].CalcBounds();

                bound = bound.GetAABB(GetMatrix());

                if(finalBound.min.x > bound.min.x)
                {
                    finalBound.min.x = bound.min.x;
                }
                if (finalBound.min.y > bound.min.y)
                {
                    finalBound.min.y = bound.min.y;
                }
                if (finalBound.min.z > bound.min.z)
                {
                    finalBound.min.z = bound.min.z;
                }
                if (finalBound.max.x < bound.max.x)
                {
                    finalBound.max.x = bound.max.x;
                }
                if (finalBound.max.y < bound.max.y)
                {
                    finalBound.max.y = bound.max.y;
                }
                if (finalBound.max.z < bound.max.z)
                {
                    finalBound.max.z = bound.max.z;
                }
            }

            //Store bounds so we only calculate this one.

            precalculatedBounds = finalBound;

            return finalBound;
        }

    }
}
