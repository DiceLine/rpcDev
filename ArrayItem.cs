using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsRPC
{
    public class ArrayItem
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsSorted { get; }
        public string DisplayText { get; }

        public ArrayItem(int id, string name, bool isSorted, string displayText)
        {
            Id = id;
            Name = name;
            IsSorted = isSorted;
            DisplayText = displayText;
        }

        public override string ToString() => DisplayText;
    }
}
