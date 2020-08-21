using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RT.Patterns;

namespace RT
{
    public class RefractiveIndex
    {
        public const double Vacuum = 1.0;
        public const double Air = 1.00029;
        public const double Water = 1.333;
        public const double Glass = 1.52;
        public const double Diamond = 2.417;
    }

    public class Material
    {

        public Pattern pattern = null;
        public Color color;
        double ambient;
        double diffuse;
        double specular;
        double shinniness;
        double reflective;
        double refractiveIndex;
        double transparency;

        public double Ambient {
            get { return ambient; }
            set {
            if(value < 0.0)
                { value = 0.0; }
            ambient = value; }
            }

        public double Diffuse
        {
            get { return diffuse; }
            set
            {
                if (value < 0.0)
                { value = 0.0; }
                diffuse = value;
            }
        }

        public double Specular
        {
            get { return specular; }
            set
            {
                if (value < 0.0)
                { value = 0.0; }
                specular = value;
            }
        }

        public double Shinniness
        {
            get { return shinniness; }
            set
            {
                if (value <= 10.0)
                { value = 10.0; }
                if(value > 200.0)
                {
                    value = 200.0;
                }
                shinniness = value;
            }
        }

        public double Reflective
        {
            get { return reflective; }
            set
            {
                if (value < 0.0)
                { value = 0.0; }

                if(value > 1.0)
                {
                    value = 1.0;
                }

                reflective = value;
            }
        }

        public double RefracIndex
        {
            get { return refractiveIndex; }
            set
            {

                refractiveIndex = value;
            }
        }

        public double Transparency
        {
            get { return transparency; }
            set
            {
                if (value < 0.0)
                { value = 0.0; }

                if (value > 1.0)
                {
                    value = 1.0;
                }

                transparency = value;
            }
        }

        public Material()
        {
            pattern = null;
            color = new Color(1, 1, 1);
            ambient = 0.1;
            diffuse = 0.9;
            specular = 0.9;
            shinniness = 200;
            transparency = 0.0;
            RefracIndex = RefractiveIndex.Air;
        }

        public Material(Color color,
                        double ambient = 0.1,
                        double diffuse = 0.9,
                        double specular = 0.9,
                        double shinniness = 200.0,
                        double transparency = 0.0,
                        double refractiveIndex = RefractiveIndex.Air,
                        StripePattern pattern = null)
        {
            this.color = color;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shinniness = shinniness;
            Transparency = transparency;
            RefracIndex = refractiveIndex;
            this.pattern = pattern;
        }


        public void Glassy()
        {
            this.transparency = 1.0;
            this.refractiveIndex = RefractiveIndex.Glass;
        }

        public override string ToString()
        {
            return "Material-> Color: " + color.ToString() +
                " Ambient: " + ambient.ToString() +
                " Diffuse: " + diffuse.ToString() +
                " Specular: " + specular.ToString() +
                " Shinniness: " + shinniness.ToString() +
                " Transparency: " + transparency.ToString() + 
                " Refractive Index: " + refractiveIndex.ToString()
                ;
        }

        public override bool Equals(object obj)
        {
            var material = obj as Material;
            return material != null &&
                   EqualityComparer<Color>.Default.Equals(color, material.color) &&
                   ambient == material.ambient &&
                   diffuse == material.diffuse &&
                   specular == material.specular &&
                   shinniness == material.shinniness &&
                   Ambient == material.Ambient &&
                   Diffuse == material.Diffuse &&
                   Specular == material.Specular &&
                   Shinniness == material.Shinniness &&
                   RefracIndex == material.RefracIndex &&
                   Transparency == material.Transparency && 
                   this.pattern == material.pattern;
        }

        public override int GetHashCode()
        {
            var hashCode = 412502415;
            hashCode = hashCode * -1521134295 + EqualityComparer<Pattern>.Default.GetHashCode(pattern);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(color);
            hashCode = hashCode * -1521134295 + ambient.GetHashCode();
            hashCode = hashCode * -1521134295 + diffuse.GetHashCode();
            hashCode = hashCode * -1521134295 + specular.GetHashCode();
            hashCode = hashCode * -1521134295 + shinniness.GetHashCode();
            hashCode = hashCode * -1521134295 + reflective.GetHashCode();
            hashCode = hashCode * -1521134295 + refractiveIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + transparency.GetHashCode();
            hashCode = hashCode * -1521134295 + Ambient.GetHashCode();
            hashCode = hashCode * -1521134295 + Diffuse.GetHashCode();
            hashCode = hashCode * -1521134295 + Specular.GetHashCode();
            hashCode = hashCode * -1521134295 + Shinniness.GetHashCode();
            hashCode = hashCode * -1521134295 + Reflective.GetHashCode();
            hashCode = hashCode * -1521134295 + RefracIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + Transparency.GetHashCode();
            return hashCode;
        }
    }
}
