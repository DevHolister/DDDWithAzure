using System.Xml.Serialization;

namespace Linde.Infrastructure.Coaching.Models;


[XmlRoot(ElementName = "UserData")]
public class UserData
{
	[XmlElement(ElementName = "NO_EMP")]
	public string NO_EMP { get; set; }
	[XmlElement(ElementName = "USUARIO")]
	public string USUARIO { get; set; }
	[XmlElement(ElementName = "USUARIO_EMP")]
	public string USUARIO_EMP { get; set; }
	[XmlElement(ElementName = "NOMBRE")]
	public string NOMBRE { get; set; }
	[XmlElement(ElementName = "CORREO_NOTES")]
	public string CORREO_NOTES { get; set; }
	[XmlElement(ElementName = "RFC")]
	public string RFC { get; set; }
	[XmlElement(ElementName = "CIA")]
	public string CIA { get; set; }
	[XmlElement(ElementName = "BU")]
	public string BU { get; set; }
	[XmlElement(ElementName = "MONTO_REQUIS")]
	public string MONTO_REQUIS { get; set; }
	[XmlElement(ElementName = "NO_EMP_AUTORIZA")]
	public string NO_EMP_AUTORIZA { get; set; }
	[XmlElement(ElementName = "EMP_PUESTO")]
	public string EMP_PUESTO { get; set; }
	[XmlElement(ElementName = "EMAIL")]
	public string EMAIL { get; set; }
	[XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
	public string Id { get; set; }
	[XmlAttribute(AttributeName = "rowOrder", Namespace = "urn:schemas-microsoft-com:xml-msdata")]
	public string RowOrder { get; set; }
}

[XmlRoot(ElementName = "NewDataSet")]
public class NewDataSet
{
	[XmlElement(ElementName = "UserData")]
	public UserData UserData { get; set; }
	[XmlAttribute(AttributeName = "xmlns")]
	public string Xmlns { get; set; }
}

[XmlRoot(ElementName = "diffgram", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
public class Diffgram
{
	[XmlElement(ElementName = "NewDataSet")]
	public NewDataSet NewDataSet { get; set; }
	[XmlAttribute(AttributeName = "msdata", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Msdata { get; set; }
	[XmlAttribute(AttributeName = "diffgr", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Diffgr { get; set; }
}

[XmlRoot(ElementName = "UserData")]
public class UserData1
{
	[XmlElement(ElementName = "USUARIO")]
	public string USUARIO { get; set; }
	[XmlElement(ElementName = "USUARIO_EMP")]
	public string USUARIO_EMP { get; set; }
	[XmlElement(ElementName = "NOMBRE")]
	public string NOMBRE { get; set; }
	[XmlElement(ElementName = "CORREO_NOTES")]
	public string CORREO_NOTES { get; set; }
	[XmlElement(ElementName = "RFC")]
	public string RFC { get; set; }
	[XmlElement(ElementName = "CIA")]
	public string CIA { get; set; }
	[XmlElement(ElementName = "BU")]
	public string BU { get; set; }
	[XmlElement(ElementName = "MONTO_REQUIS")]
	public string MONTO_REQUIS { get; set; }
	[XmlElement(ElementName = "EMP_PUESTO")]
	public string EMP_PUESTO { get; set; }
	[XmlElement(ElementName = "EMAIL")]
	public string EMAIL { get; set; }
	[XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
	public string Id { get; set; }
	[XmlAttribute(AttributeName = "rowOrder", Namespace = "urn:schemas-microsoft-com:xml-msdata")]
	public string RowOrder { get; set; }
	[XmlAttribute(AttributeName = "hasChanges", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
	public string HasChanges { get; set; }
	[XmlAttribute(AttributeName = "msdata", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Msdata { get; set; }
	[XmlAttribute(AttributeName = "diffgr", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Diffgr { get; set; }
}

[XmlRoot(ElementName = "DocumentElement")]
public class DocumentElement
{
	[XmlElement(ElementName = "UserData")]
	public UserData1 UserData { get; set; }
	[XmlAttribute(AttributeName = "xmlns")]
	public string Xmlns { get; set; }
}