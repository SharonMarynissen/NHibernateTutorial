using HibernatingRhinos.Profiler.Appender.NHibernate;   //Needed foor the NHibernate profiler app
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

using System;
using System.Linq;
using System.Reflection;
using NHibernate.Cache;

namespace NHibernateDemoApp
{

    class Program
    {
        static void Main(string[] args)
        {
            NHibernateProfiler.Initialize();        //So it will send data over NHibernate profiler app (so u can see what query is executed on db)
            var cfg = new Configuration();

            //cfg.DataBaseIntegration(x =>
            //{
            //    x.ConnectionString = @"Data Source =.\SQLSERVER2012; Initial Catalog = NHibernateDemoDB; Integrated Security = True; Connect Timeout = 15; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //    x.Driver<SqlClientDriver>();
            //    x.Dialect<MsSql2012Dialect>();
            //x.LogSqlInConsole = true;
            //});


            //cfg.Configure(@"\users\sharon\School\2de jaar\.NET - Framework\VisualStudioProjects\NhibernateTutorial1\NHN.TUT\hibernate.cfg.xml"); // --> NOT DONE!! :-)
            cfg.Configure(); //Will load the hibernate.cfg.xml file by default, but it could not find the file 
                             // --> REQUIRES: File property 'Copy to Output Directory' to be set to 'Copy ...'
                             //-- OR --
                             //cfg.Configure(Assembly.GetExecutingAssembly(), "NHibernateDemoApp.hibernate.cfg.xml"); //Will load the hibernate.cfg.xml file (using its fully qualified resource name) 
                             // --> REQUIRES: File property 'Build Action' to be set to 'Embedded Resource'

            //new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(true, true);

            //For caching
            //cfg.Cache(c =>
            //{
            //    c.UseMinimalPuts = true;
            //    c.UseQueryCache = true;
            //});

            //cfg.SessionFactory().Caching.Through<HashtableCacheProvider>().WithDefaultExpiration(1440);
            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            var sefact = cfg.BuildSessionFactory();

            using (var session = sefact.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    //perform database logic 
                    //Create 
                    //Student s1 = new Student()
                    //{
                    //    //Id = 1,       //will be done automaticly by db 
                    //    FirstName = "Allan",
                    //    LastName = "Bommer",
                    //    AcademicStanding = StudentAcademicStanding.Excellent,
                    //    Address = new Location()
                    //    {
                    //        Street = "123 Street", 
                    //        City = "Lahore", 
                    //        Province = "Punjab",
                    //        Country = "Pakistan"
                    //    }                
                    //};

                    //Student s2 = new Student()
                    //{
                    //    //Id = 2,       //will be done automaticly by db 
                    //    FirstName = "Jerry",
                    //    LastName = "Lewis",
                    //    AcademicStanding = StudentAcademicStanding.Good
                    //};

                    //session.Save(s1); //Save method from the OpenSession
                    //session.Save(s2);

                    //Read data from Student table
                    var students = session.CreateCriteria<Student>().List<Student>();
                    foreach (Student s in students)
                        Console.Write("\n{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6} \t{7}", 
                            s.Id, s.FirstName, s.LastName, s.AcademicStanding, 
                            s.Address.Street, s.Address.City, s.Address.Province, 
                            s.Address.Country);

                    ////Get a student with a certain id
                    //Student st = session.Get<Student>(1);
                    //Console.WriteLine("\nRetrieved by Id:");
                    //Console.WriteLine("\t{0} \t{1} \t{2}", st.Id, st.FirstName, st.LastName);

                    ////Update: First get the record u want to update (st with id 1)
                    ////than update the record by using the update method of OpenSession
                    //Console.WriteLine("Update the last name of Id = {0}", st.Id);
                    //st.LastName = "Donald";
                    //session.Update(st);     //Update method of OpenSession

                    //Console.WriteLine("\nRead the complete list again:");
                    //foreach (Student s in students)
                    //    Console.WriteLine("{0} \t{1} \t{2}", s.Id, s.FirstName, s.LastName);

                    ////Delete: First get the record u want to delete (st with id 1)
                    ////than update the record by using the delete method of OpenSession
                    //session.Delete(st);
                    //Console.WriteLine("\nRead the complete list again:");
                    //foreach (Student s in students)
                    //    Console.WriteLine("{0} \t{1} \t{2}", s.Id, s.FirstName, s.LastName);


                    //Getting the same student twice to illustrate caching
                    //First student executes query to db and is cached in first level cache 
                    //Before second query is fired to db NHibernate first looks up student with id 1 in 
                    //level 1 cache, it finds that student so no need to retrieve the same object again
                    //Caching is enabled by default and nothing needs to be done to work with cache.
                    //var studentUsingFirstQuery = session.Get<Student>(1);
                    //var studentUsingSecondQuery = session.Get<Student>(1);

                    tx.Commit(); //Commit() of BeginTransaction
                }

                Console.ReadLine();
            }
        }
    }
}

