using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class ObjLoader
    {
        int linesIgnored = 0;
        char[] trim = { ' ' };
        public List<Point> v = new List<Point>();
        public List<Vector> n = new List<Vector>();
        public List<Triangle> t = new List<Triangle>();

        public Group root;
        

        public int GetLinesIgnored()
        {
            return linesIgnored;
        }

        public ObjLoader()
        {
            root = new Group();
        }

        public void Load(string filename)
        {
            filename = System.AppContext.BaseDirectory + filename;

            if (System.IO.File.Exists(filename))
            {
                int lineNumber = 0;
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(filename);

                Group currentGroup = root;

                while((line = file.ReadLine()) != null)
                {
                    
                    string[] parameters = line.Trim(trim).Split(' ');

                    if(parameters[0] == "g")
                    {
                        currentGroup = new Group();
                        currentGroup.SetParent(root);
                    }

                    else if(parameters[0] == "v")
                    {
                        if (parameters.Length == 4)
                        {
                            v.Add(new Point(double.Parse(parameters[1]),
                                            double.Parse(parameters[2]),
                                            double.Parse(parameters[3])));
                        }
                        else
                        {
                            Console.WriteLine("Invalid number of points for vertex command on line " +
                                                lineNumber.ToString());
                        }
                    }

                    else if(parameters[0] == "vn")
                    {
                        if (parameters.Length == 4)
                        {
                            n.Add(new Vector(double.Parse(parameters[1]),
                                            double.Parse(parameters[2]),
                                            double.Parse(parameters[3])));
                        }
                    }

                    else if(parameters[0] == "f")
                    {
                        if(parameters.Length >= 4)
                        {

                            //Properly formed triangle that does not need separating
                            try
                            {
                                //Number of parameters that are indices.
                                int[] indices = new int[parameters.Length - 1];
                                List<int> normalIndices = new List<int>();
                                //Loop to grab each index
                                for(int i = 1; i < parameters.Length; i++)
                                {
                                    //First we need to see if this contains more
                                    //information than just the vertex index by
                                    //performing an additional split using "/"

                                    string[] faceInfo = parameters[i].Split('/');

                                    //Use the first part of the face info,
                                    //Can use the rest later when we need UVs and Normals
                                    //We are zero based, the obj file is 1 based
                                    if(faceInfo[0] != "")
                                        indices[i - 1] = int.Parse(faceInfo[0]) - 1;
                                    if(faceInfo.Length > 2 && faceInfo[2] != "")
                                        normalIndices.Add(int.Parse(faceInfo[2]) - 1);
                                }
                                //Pass to fan triangulation to generate each triangle
                                List<Triangle> triangles = FanTriangulation(indices, normalIndices);
                                //Add to group of all triangles, this may change in the future
                                //to optimize things, that's why I'm not doing it in the 
                                //Fan Triangulation method
                                for(int i = 0; i < triangles.Count; i++)
                                {
                                    triangles[i].SetParent(currentGroup);
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Unable to parse face at line " + lineNumber.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Insufficient number of indices provided to create a triangle at line " + lineNumber.ToString());
                        }
                    }
                    else
                    {
                        //everything else is ignored.
                        linesIgnored++;
                    }
                    lineNumber++;
                }
                file.Close();
            }
        }

        protected Triangle CreateTriangle(int i1, int i2, int i3)
        {
            //Makes sure the index provides 
            if (i1 < v.Count && i2 < v.Count && i3 < v.Count)
            {
                Triangle triangle = new Triangle(v[i1],
                                                    v[i2],
                                                    v[i3]);
                t.Add(triangle);
                return triangle;
            }
            return null;
        }

        protected List<Triangle> FanTriangulation(int[] i, List<int> ni)
        {
            List<Triangle> triangles = new List<Triangle>();

            if (i.Length >= 3)
            {
                //Starting at the second vertex go to one minus the end because we're always
                //accessing an element one ahead of us to create the triangles.
                for (int x = 1; x < i.Length - 1; x++)
                {

                    Triangle triangle = CreateTriangle(i[0],
                                                        i[x],
                                                        i[x + 1]);
                    //If normals exist, we add them here...
                    if (ni.Count >= 3)
                    {
                        triangle.n1 = n[ni[0]];
                        triangle.n2 = n[ni[x]];
                        triangle.n3 = n[ni[x + 1]];
                    }

                    triangles.Add(triangle);
                }
            }
            return triangles;
        }


    }
}
