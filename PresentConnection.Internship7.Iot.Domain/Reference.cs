using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PresentConnection.Internship7.Iot.Domain
{
    /*
    public class ReferenceItem
    {
        public string Id { get; set; }
        public string CollectionName { get; set; }
    }

    public class Refences : ICollection<Reference>
    {
        public IEnumerator<Reference> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Reference item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(Reference item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(Reference[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Reference item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
    }


    public class ReferenceBase // : ICollection
    {
        protected ReferenceBase(string collectionName, List<string> ids, bool loadImmediately = false)
        {
            CollectionName = collectionName;
            Ids = ids;
            LoadImmediately = loadImmediately;
        }

        protected List<string> Ids { get; }

        protected string CollectionName { get; }

        protected bool LoadImmediately { get; }
    }


    public class Reference : ReferenceBase
    {
        public Reference(string id, string collectionName, bool loadImmediately = false) : base(collectionName, new List<string> { id}, loadImmediately)
        {
            
        }
        
        public string Id
        {
            get
            {
                if (Ids != null && Ids.Any())
                {
                    return Ids.FirstOrDefault();
                }
                return string.Empty;
            }
        }
    }

    public class References : ReferenceBAse
    {
        public References(string collectionName, List<string> ids, bool loadImmediately = false) : base(collectionName, new List<string> { id}, loadImmediately)
        {
            
        }
        
        public string Id
        {
            get
            {
                if (Ids != null && Ids.Any())
                {
                    return Ids.FirstOrDefault();
                }
                return string.Empty;
            }
        }
    }
    */
}