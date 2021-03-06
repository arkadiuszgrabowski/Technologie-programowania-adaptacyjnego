﻿using Library.Reflection;
using Library.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TreeView
{
    public class ParameterTI : TreeViewItem
    {
        public ParameterMetadata ParameterMetadata { get; set; }

        public ParameterTI(ParameterMetadata parameterMetadata, ItemTypeEnum type) : base(parameterMetadata.Name, type)
        {
            ParameterMetadata = parameterMetadata;
        }

        protected override void BuildMyself(ObservableCollection<TreeViewItem> children)
        {
            if (ParameterMetadata.Type != null)
            {
                children.Add(new TypeTI(TypeSingleton.Instance.Get(ParameterMetadata.Type.Name), ItemTypeEnum.Type));
            }
        }
    }
}
