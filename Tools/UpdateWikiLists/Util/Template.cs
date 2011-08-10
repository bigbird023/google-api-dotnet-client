﻿/*
Copyright 2011 Google Inc

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace UpdateWikiLists.Util
{
    /// <summary>
    /// Template helper class
    /// </summary>
    internal class Template : List<string>
    {
        public Template(string data)
        {
            AddRange(data.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
        }
        public Template() {}

        protected virtual string NewLine { get { return Environment.NewLine; } }

        /// <summary>
        /// Returns the formatted result of this template/
        /// </summary>
        public string ToString(Entries values)
        {
            string result = ToString();
            foreach (KeyValuePair<string, object> value in values)
            {
                result = result.Replace("{" + value.Key + "}", value.Value == null ? "" : value.Value.ToString());
            }
            return result;
        }
        public override string ToString()
        {
            return this.Aggregate((a, b) => a + NewLine + b);
        }
    }

    internal class Entries : Dictionary<string, object>
    {
        /// <summary>
        /// Adds the formatted string to this template.
        /// </summary>
        public void Add(string key, string format, params object[] values)
        {
            if (values.Length == 0 || string.IsNullOrEmpty(format))
            {
                Add(key, (object) format);
                return;
            }
            Add(key, (object)string.Format(format, values));
        }
    }
}