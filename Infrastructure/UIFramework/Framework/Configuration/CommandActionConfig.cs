using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Controls.Framework;

namespace Controls.Framework
{
    [Serializable]
    [XmlRoot("CommandConfig")]
    public class CommandConfig : ISerializable
    {
        [XmlAttribute("key")]
        public string CommandKey { get; set; }

        [XmlAttribute("uri")]
        public string CommandUri { get; set; }

        public CommandConfig()
        {

        }

        public CommandConfig(SerializationInfo info, StreamingContext context)
        {
            this.CommandKey = (string)info.GetValue("CommandKey", typeof(string));
            this.CommandUri = (string)info.GetValue("CommandUri", typeof(string));
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CommandKey", this.CommandKey, typeof(string));
            info.AddValue("CommandUri", this.CommandUri, typeof(string));
        }
    }

    [Serializable]
    public class CommandActionConfig : ISerializable
    {
        [XmlAttribute("key")]
        public string ActionKey { get; set; }

        [XmlAttribute("command")]
        public string CommandConfig { get; set; }

        [XmlAttribute("result")]
        public string ResultType { get; set; }

        [XmlAttribute("refreshDiv")]
        public string RefreshDiv { get; set; }

        [XmlAttribute("view")]
        public string ViewName { get; set; }

        [XmlAttribute("allowAnonymous")]
        public bool AllowAnonymous { get; set; }

        [XmlAttribute("exceptionPolicy")]
        public string ExceptionPolicy { get; set; }

        [XmlAttribute("taskId")]
        public int TaskId { get; set; }

        public CommandActionConfig()
        {

        }

        public CommandActionConfig(SerializationInfo info, StreamingContext context)
        {
            this.ActionKey = (string)info.GetValue("ActionKey", typeof(string));
            this.CommandConfig = (string)info.GetValue("CommandConfig", typeof(CommandConfig));
            this.ResultType = (string)info.GetValue("ResultType", typeof(string));
            this.RefreshDiv = (string)info.GetValue("RefreshDiv", typeof(string));
            this.ViewName = (string)info.GetValue("ViewName", typeof(string));
            this.AllowAnonymous = (bool)info.GetValue("AllowAnonymous", typeof(bool));
            this.ExceptionPolicy = (string)info.GetValue("ExceptionPolicy", typeof(string));
            this.TaskId = (int)info.GetValue("TaskId", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ActionKey", this.ActionKey, typeof(string));
            info.AddValue("CommandConfig", this.CommandConfig, typeof(CommandConfig));
            info.AddValue("ResultType", this.ResultType, typeof(string));
            info.AddValue("RefreshDiv", this.RefreshDiv, typeof(string));
            info.AddValue("ViewName", this.ViewName, typeof(string));
            info.AddValue("AllowAnonymous", this.AllowAnonymous, typeof(bool));
            info.AddValue("ExceptionPolicy", this.ExceptionPolicy, typeof(string));
            info.AddValue("TaskId", this.TaskId, typeof(int));
        }
    }

    [Serializable]
    public class CommandActionConfigBag : ISerializable
    {
        public List<CommandActionConfig> ActionConfig { get; set; }

        public CommandActionConfigBag()
        {
            ActionConfig = new List<CommandActionConfig>();
        }

        public CommandActionConfigBag(SerializationInfo info, StreamingContext context)
        {
            this.ActionConfig = (List<CommandActionConfig>)info.GetValue("ActionConfig", typeof(List<CommandActionConfig>));

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ActionConfig", this.ActionConfig, typeof(List<CommandActionConfig>));
        }
    }

}
