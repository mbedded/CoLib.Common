using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CoLib.Common.Collections {

  /// <summary>
  ///   This class represents a Serializable dictionary.
  ///   The "normal" one given by .NET-Framework cannot be serialized and de-serialized.
  /// </summary>
  [Serializable]
  [XmlRoot(XML_ROOT)]
  public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable {

    private const string XML_ROOT = "SerializableDictionary";

    private const string DEFAULT_TAG_ITEM = "Item";
    private const string DEFAULT_TAG_KEY = "Key";
    private const string DEFAULT_TAG_VALUE = "Value";

    private readonly XmlSerializer _keySerializer = new XmlSerializer(typeof(TKey));
    private readonly XmlSerializer _valueSerializer = new XmlSerializer(typeof(TValue));

    /// <inheritdoc />
    public XmlSchema GetSchema() {
      return null;
    }

    /// <inheritdoc />
    public void ReadXml(XmlReader reader) {
      bool isEmpty = reader.IsEmptyElement;

      // The header line or root-element
      reader.Read();

      // Read again if first line was a header
      if (reader.NodeType == XmlNodeType.XmlDeclaration) {
        reader.Read();
      }

      // Read again if it is the root element. Otherwise the following code will cause an error
      if (string.Equals(reader.Name, XML_ROOT, StringComparison.Ordinal)) {
        reader.Read();
      }


      if (isEmpty) {
        return;
      }

      while (reader.NodeType != XmlNodeType.EndElement) {
        reader.ReadStartElement(DEFAULT_TAG_ITEM);

        try {
          TKey key;
          TValue value;

          reader.ReadStartElement(DEFAULT_TAG_KEY);

          try {
            key = (TKey) _keySerializer.Deserialize(reader);
          } finally {
            reader.ReadEndElement();
          }

          reader.ReadStartElement(DEFAULT_TAG_VALUE);

          try {
            value = (TValue) _valueSerializer.Deserialize(reader);
          } finally {
            reader.ReadEndElement();
          }

          Add(key, value);
        } finally {
          reader.ReadEndElement();
        }

        reader.MoveToContent();
      }

      reader.ReadEndElement();
    }

    /// <inheritdoc />
    public void WriteXml(XmlWriter writer) {
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this) {
        writer.WriteStartElement(DEFAULT_TAG_ITEM);
        writer.WriteStartElement(DEFAULT_TAG_KEY);

        try {
          _keySerializer.Serialize(writer, keyValuePair.Key);
        } finally {
          writer.WriteEndElement();
        }

        writer.WriteStartElement(DEFAULT_TAG_VALUE);

        try {
          _valueSerializer.Serialize(writer, keyValuePair.Value);
        } finally {
          writer.WriteEndElement();
        }

        writer.WriteEndElement();
      }
    }

  }

}
