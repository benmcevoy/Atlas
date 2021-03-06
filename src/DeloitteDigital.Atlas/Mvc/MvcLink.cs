﻿using System;
using System.Web.Mvc;
using DeloitteDigital.Atlas.FieldRendering;

namespace DeloitteDigital.Atlas.Mvc
{
    internal class EmptyMvcLink : IDisposable
    {
        public void Dispose()
        {
        }
    }

    internal class MvcLink : IDisposable
    {
        private readonly ViewContext viewContext;
        private readonly TagBuilder tagBuilder;

        public MvcLink(ViewContext viewContext, ILinkFieldRenderingString linkField, string alternateTag)
        {
            this.viewContext = viewContext;

            if (linkField?.Url != null)
            {
                // link given - render an anchor tag
                this.tagBuilder = new TagBuilder("a");
                this.tagBuilder.Attributes.Add("href", linkField.Url);
                // add optional attributes
                if (!string.IsNullOrWhiteSpace(linkField.Target))
                    this.tagBuilder.Attributes.Add("target", linkField.Target);
                if (!string.IsNullOrWhiteSpace(linkField.Class))
                    this.tagBuilder.Attributes.Add("class", linkField.Class);
                if (!string.IsNullOrWhiteSpace(linkField.Description))
                    this.tagBuilder.Attributes.Add("title", linkField.Description);
            }
            else if (string.IsNullOrWhiteSpace(alternateTag))
            {
                // no link given - render the alternate tag if provided
                this.tagBuilder = new TagBuilder(alternateTag);
            }

            // render the opening tag
            if (this.tagBuilder != null)
                viewContext.Writer.Write(this.tagBuilder.ToString(TagRenderMode.StartTag));
        }

        public void Dispose()
        {
            // render the closing tag
            if (this.tagBuilder != null)
                this.viewContext.Writer.Write(this.tagBuilder.ToString(TagRenderMode.EndTag));
        }
    }
}
