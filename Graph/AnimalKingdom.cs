using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class AnimalKingdom
    {
        public string ParentName;
        public List<AnimalKingdom> Childrens;

        public AnimalKingdom(string parent)
        {
            ParentName = parent;
            Childrens = new List<AnimalKingdom>();
        }

        public void AddChild(AnimalKingdom child) => Childrens.Add(child);
    }

    public class Relation
    {
        public string parent;
        public string child;
        public Relation(string p, string c)
        {
            parent = p;
            child = c;
        }

        public static List<Relation> GetRelations()
        {
            var relations = new List<Relation>();
            relations.Add(new Relation("animal", "mammal"));
            relations.Add(new Relation("animal", "bird"));
            relations.Add(new Relation("lifeform", "animal"));
            relations.Add(new Relation("cat", "lion"));
            relations.Add(new Relation("mammal", "cat"));
            relations.Add(new Relation("animal", "fish"));

            return relations;
        }
    }
}
