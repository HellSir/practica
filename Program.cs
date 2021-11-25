using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEPROGA
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    var pt = from p in model.ProductType
                             select p;

                    ProductType productType = null;
                    Random rnd = new Random();

                    foreach (var item in pt)
                    {

                        productType = model.ProductType.Where(a => a.ID == item.ID).FirstOrDefault();
                        productType.DefectedPercent = rnd.Next(60);

                        model.Entry(productType).State = System.Data.Entity.EntityState.Modified;
                    }

                    model.SaveChanges();

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private static void DelAgent()
        {
            using (ModelBD model = new ModelBD())
            {

                var querry = from a in model.Agent
                             select a;

                Agent agent = null;

                foreach (var item in querry)
                {
                    agent = model.Agent.Where(p => p.ID == item.ID).FirstOrDefault();
                    agent.AgentTypeID = int.Parse(agent.ID.ToString());

                    model.Agent.Remove(agent);
                    model.Entry(agent).State = System.Data.Entity.EntityState.Modified;
                }


                model.SaveChanges();

            }
        }

        private static void Other()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    var agents = from p in model.Agent
                                 select p;

                    var agentsTypes = from a in model.AgentType
                                      select a;

                    Agent agent = null;

                    int[] ids = new int[100];
                    int count = 0;
                    foreach (var agentsType in agentsTypes)
                    {
                        ids[count] = agentsType.ID;
                        count++;
                    }


                    count = 0;

                    Random rnd = new Random();

                    foreach (var item in agents)
                    {

                        agent = model.Agent.Where(a => a.ID == item.ID).FirstOrDefault();
                        agent.AgentTypeID = ids[count];

                        model.Entry(agent).State = System.Data.Entity.EntityState.Modified;

                        count++;
                    }

                    model.SaveChanges();

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
