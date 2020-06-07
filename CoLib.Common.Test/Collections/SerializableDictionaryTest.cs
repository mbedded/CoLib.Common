using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CoLib.Common.Collections;
using Xunit;

namespace CoLib.Common.Test.Collections {
  public class SerializableDictionaryTest {

    private const string SERIALIZED_DICTIONARY = "<?xml version=\"1.0\" encoding=\"utf-16\"?><SerializableDictionary><Item><Key><string>MyKey</string></Key><Value><string>MyValue</string></Value></Item></SerializableDictionary>";
    private const string SERIALIZED_DICTIONARY_WITHOUT_HEADER = "<SerializableDictionary><Item><Key><string>MyKey</string></Key><Value><string>MyValue</string></Value></Item></SerializableDictionary>";

    private SerializableDictionary<string, string> GetInstance() {
      return new SerializableDictionary<string, string>();
    }

    [Fact]
    public void CreateInstance() {
      var target = GetInstance();

      Assert.NotNull(target);
    }


    [Fact]
    public void Serialize_to_XML() {
      var target = GetInstance();

      target.Add("MyKey", "MyValue");

      var builder = new StringBuilder();
      var xmlWriter = XmlWriter.Create(builder);
      var serializer = new XmlSerializer(target.GetType());
      serializer.Serialize(xmlWriter, target);

      string result = builder.ToString();

      Assert.Equal(SERIALIZED_DICTIONARY, result);
    }

    [Fact]
    public void Deserialize_from_XML_with_header() {
      var target = GetInstance();

      var xmlReader = new XmlTextReader(new StringReader(SERIALIZED_DICTIONARY));
      target.ReadXml(xmlReader);

      Assert.True(target.ContainsKey("MyKey"));
      Assert.Equal("MyValue", target["MyKey"]);
    }

    [Fact]
    public void Deserialize_from_XML_without_header() {
      var target = GetInstance();

      var xmlReader = new XmlTextReader(new StringReader(SERIALIZED_DICTIONARY_WITHOUT_HEADER));
      target.ReadXml(xmlReader);

      Assert.True(target.ContainsKey("MyKey"));
      Assert.Equal("MyValue", target["MyKey"]);
    }


  }
}
