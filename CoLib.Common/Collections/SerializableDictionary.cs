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

    private static readonly XmlSerializer _keySerializer = new XmlSerializer(typeof(TKey));
    private static readonly XmlSerializer _valueSerializer = new XmlSerializer(typeof(TValue));

    /// <inheritdoc />
    public SerializableDictionary()
        : base() {
    }

    /// <inheritdoc />
    public XmlSchema GetSchema() {
      return null;
    }

    /// <inheritdoc />
    public void ReadXml(XmlReader xReader) {
      bool isEmpty = xReader.IsEmptyElement;

      // The header line or root-element
      xReader.Read();

      // Read again if first line was a header
      if (xReader.NodeType == XmlNodeType.XmlDeclaration) {
        xReader.Read();
      }
      // Read again if it is the root element. Otherwise the following code will cause an error
      if (string.Equals(xReader.Name, XML_ROOT, StringComparison.Ordinal)) {
        xReader.Read();
      }


      if (isEmpty) {
        return;
      }

      while (xReader.NodeType != XmlNodeType.EndElement) {
        xReader.ReadStartElement(DEFAULT_TAG_ITEM);
        try {
          TKey lKey;
          TValue lValue;

          xReader.ReadStartElement(DEFAULT_TAG_KEY);
          try {
            lKey = (TKey)_keySerializer.Deserialize(xReader);
          } finally {
            xReader.ReadEndElement();
          }

          xReader.ReadStartElement(DEFAULT_TAG_VALUE);
          try {
            lValue = (TValue)_valueSerializer.Deserialize(xReader);
          } finally {
            xReader.ReadEndElement();
          }

          Add(lKey, lValue);
        } finally {
          xReader.ReadEndElement();
        }

        xReader.MoveToContent();
      }

      xReader.ReadEndElement();
    }

    /// <inheritdoc />
    public void WriteXml(XmlWriter xWriter) {
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this) {
        xWriter.WriteStartElement(DEFAULT_TAG_ITEM);
        xWriter.WriteStartElement(DEFAULT_TAG_KEY);

        try {
          _keySerializer.Serialize(xWriter, keyValuePair.Key);
        } finally {
          xWriter.WriteEndElement();
        }

        xWriter.WriteStartElement(DEFAULT_TAG_VALUE);

        try {
          _valueSerializer.Serialize(xWriter, keyValuePair.Value);
        } finally {
          xWriter.WriteEndElement();
        }

        xWriter.WriteEndElement();
      }

    }

  }



}
