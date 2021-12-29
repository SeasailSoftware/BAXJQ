using System;
using System.Xml;
using System.Collections.Generic;

namespace HPT.Gate.Utils.Common
{
    /// <summary>      
    /// XmlHelper 的摘要说明   
    /// </summary>
    public class XmlHelper
    {
        public XmlHelper()
        {

        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        public static string Read(string path, string node, string attribute)
        {
            var value = string.Empty;
            try
            {
                var doc = new XmlDocument();
                doc.Load(path);
                var xn = doc.SelectSingleNode(node);
                value = (attribute.Equals(string.Empty) ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch
            {

            }
            return value;

        }


        public
        static
        void
        Insert(string
        path,
        string
        node,
        string
        node1,
        string
        element,
        string
        attribute,
        string
        value,
        string
        attribute1,
        string
        value1)








        {












            try












            {
















                var
                doc
                =
                new
                XmlDocument();















                doc.Load(path);
















                var xn = doc.SelectSingleNode(node);
















                var xel = doc.CreateElement(node1);
































                var xe = doc.CreateElement(element);















                if (
                attribute.Equals(string.Empty))
                {



















                    xe.InnerText = value;
                }















                else
                {



















                    xe.SetAttribute(attribute, value);
                }















                xe.SetAttribute(attribute1, value1);















                xel.AppendChild(xe);















                xn.AppendChild(xel);































                doc.Save(path);












            }











            catch
            {

            }







        }







        /// <summary>








        /// 插入数据








        /// </summary>








        /// <param name="path">路径</param>








        /// <param name="node">节点</param>








        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>








        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>








        /// <param name="value">值</param>








        /// <returns></returns>
















        public static void Insert(string path, string node, string element, string attribute, string value)
        {



















            try
            {



























                var doc = new XmlDocument();















                doc.Load(path);















                var xn = doc.SelectSingleNode(node);















                if (
                element.Equals(string.Empty))
                {



































                    if (
                    !attribute.Equals(string.Empty))
                    {











































                        var xe = (XmlElement)xn;























                        xe.SetAttribute(attribute, value);



















                    }















                }















                else
                {



































                    var xe = doc.CreateElement(element);



















                    if (
                    attribute.Equals(string.Empty))
                    {























                        xe.InnerText = value;
                    }



















                    else
                    {























                        xe.SetAttribute(attribute,
                        value);
                    }



















                    xn.AppendChild(xe);
















                }
















                doc.Save(path);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="parentNode"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            var node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {

                var doc = new XmlDocument();
                doc.Load(path);
                var xn = doc.SelectSingleNode(node);
                var xe = (XmlElement)xn;
                if (
                attribute.Equals(string.Empty))
                {
                    xe.InnerText = value;
                }
                else
                {
                    xe.SetAttribute(attribute, value);
                }
                doc.Save(path);
            }
            catch
            {

            }

        }



        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(path);

                var xnl = doc.SelectSingleNode(node).ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    var xnl1 = xn.SelectSingleNode("RedirectUrl").ChildNodes;
                    for (var j = 0; j < xnl1.Count; j++)
                    {
                        var xe1 = (XmlElement)xnl1.Item(j);
                        if (xe1.GetAttribute("UrlName") == "小路工作室1231231231")
                        {
                            xn.RemoveChild(xe1);
                            if (j < xnl1.Count)
                            {
                                j = j - 1;
                            }
                        }
                    }
                }

                doc.Save(path);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 删除属性带key的节点,key:GUID
        /// </summary>
        /// <param name="path">XML文件路径</param>
        /// <param name="rootnode">根节点</param>
        /// <param name="key">key:Guid</param>
        /// <param name="msg">返回的信息</param>
        public static void DeleteXMLNode(string path, string rootnode, string znode, string type, string key)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(path);
                    var xl = xmlDoc.SelectSingleNode(rootnode).ChildNodes;
                    for (var i = 0; i < xl.Count; i++)
                    {
                        var xe = (XmlElement)xl[i];
                        var node = xe.GetElementsByTagName(znode);
                        if (node.Count > 0)
                        {
                            for (var j = 0; j < node.Count; j++)
                            {
                                node.Item(j);
                                var xe1 = (XmlElement)node.Item(j);
                                if (xe1.GetAttribute(type) == key)
                                {
                                    xmlDoc.SelectSingleNode(rootnode).RemoveChild(node[0].ParentNode);
                                    break;
                                }
                            }
                        }
                    }

                    xmlDoc.Save(path);
                }
            }
            catch (XmlException ex)
            {
                Console.Write(ex.Message);
            }
        }
        /// <summary>
        /// 返回符合指定名称的节点数
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <returns>节点数</returns>
        public static int Count(string path, string nodeName, string znode)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                var xe = (XmlElement)xmlDoc.SelectSingleNode(nodeName);
                var nodeList = xe.GetElementsByTagName(znode);
                return nodeList.Count;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        /// <summary>
        /// 返回最后的数字
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nodeName"></param>
        /// <param name="znode"></param>
        /// <returns></returns>
        public static string num(string path, string nodeName, string znode, string type)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                var xe = (XmlElement)xmlDoc.SelectSingleNode(nodeName);
                var nodeList = xe.GetElementsByTagName(znode);
                var xe1 = (XmlElement)nodeList.Item(nodeList.Count - 1);
                var inum = xe1.GetAttribute(type);

                return inum;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
    }
}

