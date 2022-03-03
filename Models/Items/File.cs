using System;

namespace Boomerang.Models
{
    public class File : Item
    {
        public byte[] FileByteArr { get; set; }
        public string FileType { get; set; }
    }
}
