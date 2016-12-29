
using System;

namespace BananaLib.Garena
{
  [AttributeUsage(AttributeTargets.Field)]
  internal class ProtoFieldAttribute : Attribute
  {
    private DataType KeyType { get; }

    private Label Label { get; }

    private int Size { get; }

    private int Tag { get; }

    private DataType Type { get; }

    public ProtoFieldAttribute(int tag, DataType type = DataType.MESSAGE, int size = 0, Label label = Label.REQUIRED, DataType keyType = DataType.MESSAGE)
    {
      this.Tag = tag;
      this.Type = type;
      this.Size = size;
      this.Label = label;
      this.KeyType = keyType;
    }
  }
}
