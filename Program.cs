using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRayTracerChallenge
{
    class Program
    {
        public static void Tick(RT.Projectile proj, RT.Environment env)
        {
            proj.position = proj.position + proj.velocity;
            Console.WriteLine(proj.position);
            proj.velocity = proj.velocity + env.gravity + env.wind;
        }

        public static void RunSimulation()
        {
            int currentIteration = 0;
            int maxIteration = 60000;

            RT.Canvas canvas = new RT.Canvas(1000,500);

            RT.Projectile projectile = new RT.Projectile(new RT.Point(0.0f, 0.0f, 0.0f),
                                                        new RT.Vector(25.0f, 50.0f, 0.0f));
            RT.Environment environment = new RT.Environment(new RT.Vector(0.0f, -3.0f, 0.0f),
                                                            new RT.Vector(0.0f, 0.0f, 0.0f));

            while (projectile.position.y >= 0.0f && currentIteration < maxIteration)
            {
                Tick(projectile, environment);
                currentIteration++;
                //Draw to canvas
                int x = (int)projectile.position.x;
                int y = (int)projectile.position.y;
                canvas.SetPixel(x, y, RT.Color.green);
            }

            if(currentIteration == maxIteration)
            {
                Console.WriteLine("Error, max iteration count exceeded.");
            }
            
            Console.WriteLine("--Simulation Results--");
            Console.WriteLine("Projectile: " + projectile.ToString());
            Console.WriteLine("Environment: " + environment.ToString());
            RT.Save.SaveCanvas(canvas);
        }
        
        public static void TransformChallenge()
        {
            //Create canvas of set size and width
            //Create Transform that first offsets a point by 1/2 canvas size then
            //rotates the object by 1/12th of 2*pi through 12 iterations
            //At each location draw to the canvas a circle
            int circleRadius = 5;
            RT.Canvas canvas = new RT.Canvas(100, 100);

            RT.Point currentLocation = new RT.Point();

            //Offset 1/3 distance of canvas size;
            currentLocation = RT.Mat4.TranslateMatrix(canvas.GetWidth() * 0.3f,
                                                        0.0f,
                                                        0.0f)
                                                        * currentLocation;
            //Rotate loop
            int maxIterations = 12;
            for(int r = 0; r < maxIterations; r++)
            {
                currentLocation = RT.Mat4.RotateZMatrix(2.0f * RT.Constants.pi * (1.0f / maxIterations)) * currentLocation;

                //Offset current location so that it is centered in the image by 1/2 width and height through translation
                RT.Point screenSpaceLocation = RT.Mat4.TranslateMatrix(canvas.GetWidth() * 0.5f,
                                                            canvas.GetHeight() * 0.5f,
                                                            0.0f) * currentLocation;

                Console.WriteLine("Point " + r.ToString());
                Console.WriteLine(screenSpaceLocation);

                //Draw circle at current location
                canvas.DrawCircle(  (int)screenSpaceLocation.x,
                                    (int)screenSpaceLocation.y,
                                    circleRadius,
                                    RT.Color.green);
            }

            //Save out the image when completed.
            RT.Save.SaveCanvas(canvas, "TransformChallenge");
        }

        public static void Chapter5Challenge()
        {
            //Create an image of a sphere by only testing for hits or misses.
            RT.Mat4 transMatrix = new RT.Mat4();
            RT.Scene scene = new RT.Scene();
            //transMatrix = RT.Mat4.ScaleMatrix(1,0.5f,1);
            //transMatrix = RT.Mat4.ScaleMatrix(0.5f,1,1);
            //transMatrix = RT.Mat4.RotateMatrix(0.0f, 0.0f, RT.Constants.pi * 0.25f) * RT.Mat4.ScaleMatrix(1,0.5f,1);
            transMatrix = RT.Mat4.ShearMatrix(1,0,0,0,0,0) * RT.Mat4.ScaleMatrix(0.5f, 1, 1);

            int resolution = 200;
            RT.Canvas canvas = new RT.Canvas(resolution, resolution);
            canvas.FillCanvas(RT.Color.black);

            RT.Sphere sphere = new RT.Sphere();
            sphere.SetMatrix(transMatrix);

            RT.Point camera = new RT.Point(0,0,-5);

            //Use the wall x and y as the width and height and the position of the wall as the z value
            RT.Point wall = new RT.Point(0.0f,0.0f,7f);
            double wallSize = 7.0f;

            //Camera is the start point, rays are created by taking iterating over the wall in resultion steps 
            //vertically and horizontally, calc wall - camera to get direction of camera to wall location.
            //Check if the ray hits the sphere, if it does draw red if it does not draw black.

            for(int y = 0; y < resolution; y ++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    //Need to start at half the width over from the walls origin and increment from there

                    double increment = wallSize / resolution;

                    RT.Vector currentWallPixel = wall - new RT.Point((wallSize * 0.5f) - x * increment,
                                                                    (wallSize * 0.5f) - y * increment,
                                                                     wall.z);

                    //This presents a problem when I want to convert a point to a vector...
                    RT.Point point = (currentWallPixel - camera);
                    RT.Vector direction = new RT.Vector(point).Normalize();

                    RT.Ray ray = new RT.Ray(camera, direction);

                    RT.Intersection hit = RT.Scene.current.Hit(scene.Intersections( ray ));

                    if (hit != null)
                    {
                        canvas.SetPixel(x, y, RT.Color.red);
                    }
                }
            }
            RT.Save.SaveCanvas(canvas, "Chapter5Challenge");

        }
        
        public static void Chapter6Sphere()
        {
            RT.Scene scene = new RT.Scene();
            //Create an image of a sphere by only testing for hits or misses.
            RT.Mat4 transMatrix = new RT.Mat4();
            //transMatrix = RT.Mat4.ScaleMatrix(1,0.5f,1);
            //transMatrix = RT.Mat4.ScaleMatrix(0.5f,1,1);
            //transMatrix = RT.Mat4.RotateMatrix(0.0f, 0.0f, RT.Constants.pi * 0.25f) * RT.Mat4.ScaleMatrix(1,0.5f,1);
            //transMatrix = RT.Mat4.ShearMatrix(1, 0, 0, 0, 0, 0) * RT.Mat4.ScaleMatrix(0.5f, 1, 1);

            int resolution = 512;
            RT.Canvas canvas = new RT.Canvas(resolution, resolution);
            canvas.FillCanvas(RT.Color.black);

            RT.Light light = new RT.Light(new RT.Point(-10, 10, -10), RT.Color.white);
           

            RT.Sphere sphere = new RT.Sphere();
            sphere.material.color = new RT.Color(1, 0.2f, 1.0f);
            sphere.material.Ambient = 0.1f;
            sphere.material.Diffuse = 0.9f;
            sphere.material.Specular = 0.9f;

            sphere.SetMatrix(transMatrix);

            RT.Point camera = new RT.Point(0, 0, -5);

            //Use the wall x and y as the width and height and the position of the wall as the z value
            RT.Point wall = new RT.Point(0.0f, 0.0f, 7f);
            double wallSize = 7.0f;

            //Camera is the start point, rays are created by taking iterating over the wall in resultion steps 
            //vertically and horizontally, calc wall - camera to get direction of camera to wall location.
            //Check if the ray hits the sphere, if it does draw red if it does not draw black.

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    //Need to start at half the width over from the walls origin and increment from there

                    double increment = wallSize / resolution;

                    RT.Vector currentWallPixel = wall - new RT.Point((wallSize * 0.5f) - x * increment,
                                                                    (wallSize * 0.5f) - y * increment,
                                                                     wall.z);

                    //This presents a problem when I want to convert a point to a vector...
                    RT.Point point = (currentWallPixel - camera);
                    RT.Vector direction = new RT.Vector(point).Normalize();

                    RT.Ray ray = new RT.Ray(camera, direction);

                    RT.Intersection hit = RT.Scene.current.Hit(scene.Intersections(ray));


                    if (hit != null)
                    {
                        RT.Point hitPosition = ray.Position(hit.t);
                        RT.Color lighting = sphere.Lighting(    hitPosition,
                                                                light,
                                                                -ray.direction,
                                                                sphere.GetNormal(hitPosition));
                        canvas.SetPixel(x, y, lighting);
                    }
                }
            }
            RT.Save.SaveCanvas(canvas, "Chapter6Sphere");

        }

        static void Main(string[] args)
        {
            //RT.UnitTesting.Chapter7Test chapter7Test = new RT.UnitTesting.Chapter7Test();
            //chapter7Test.T21_PuttingItAllTogether();

            //RT.UnitTesting.Chapter9Test chapter9Test = new RT.UnitTesting.Chapter9Test();
            //chapter9Test.T07_PuttingItTogether();

            //RT.UnitTesting.Chapter10Test chapter10Test = new RT.UnitTesting.Chapter10Test();
            //chapter10Test.T09_BasicScene();

            //RT.UnitTesting.Chapter10Test chapter10Test = new RT.UnitTesting.Chapter10Test();
            //chapter10Test.BlendTPatterns();

            //RT.UnitTesting.Chapter11Test chapter11Test = new RT.UnitTesting.Chapter11Test();
            //chapter11Test.T21_UnderwaterScene();

            //RT.UnitTesting.Chapter12Test chapter12Test = new RT.UnitTesting.Chapter12Test();
            //chapter12Test.T04_PuttingItAllTogether();

            //RT.UnitTesting.Chapter13Test chapter13Test = new RT.UnitTesting.Chapter13Test();
            //chapter13Test.T12_IceCreamCone();

            //RT.UnitTesting.Chapter14Test chapter14Test = new RT.UnitTesting.Chapter14Test();
            //chapter14Test.T13_PuttingItAllTogether();

            RT.UnitTesting.Chapter15Test chapter15Test = new RT.UnitTesting.Chapter15Test();
            chapter15Test.T11_MayaOBJFile();

            //RT.UnitTesting.Chapter16Test chapter16Test = new RT.UnitTesting.Chapter16Test();
            //chapter16Test.T08_PuttingItTogether();

            //Console.ReadKey();
        }
    }
}
