using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HtmlBuilder
{
    public class HtmlTable
    {
        public HtmlElement Root { get; } = new HtmlElement("table");
        public HtmlElement Header { get; } = new HtmlElement("thead");
        public HtmlElement Body { get; } = new HtmlElement("tbody");
        public HtmlElement Footer { get; } = new HtmlElement("tfoot");
        public List<HtmlElement> Rows { get; } = new List<HtmlElement>();
        public List<string> Columns { get; set; }

        public HtmlTable()
        {
            Root.Children.Add(Header);
            Root.Children.Add(Body);
            Root.Children.Add(Footer);
        }

        public void AddRows(bool addColumns, int rowsToAdd)
        {
            for (int i = 0; i <= rowsToAdd - 1; i++)
            {
                HtmlElement newRow = new HtmlElement("tr");
                Body.Children.Add(newRow);
                Rows.Add(newRow);
                if (addColumns)
                {
                    for (int j = 0; j <= Columns.Count - 1; j++)
                    {
                        newRow.Children.Add(new HtmlElement("td"));
                    }
                }
            }
        }

        public HtmlElement AddRow(List<string> columnData)
        {
            HtmlElement newRow = new HtmlElement("tr");
            Body.Children.Add(newRow);
            Rows.Add(newRow);

            for (int i = 0; i <= columnData.Count - 1; i++)
            {
                newRow.Children.Add(new HtmlElement("td"));
                newRow.GetChildrenByTagName("td")[newRow.GetChildrenByTagName("td").Count - 1].Children.Add(new HtmlContent(columnData[i]));
            }

            return newRow;
        }

        public HtmlElement GetCellFromColumn(HtmlElement row, int columnIndex)
        {
            return row.Children[columnIndex];
        }

        public void CreateHeaderFromColumns()
        {
            Header.Children.Clear();
            Header.Children.Add(new HtmlElement("tr"));
            HtmlElement firstRow = Header.GetChildrenByTagName("tr")[0];
            for (int i = 0; i <= Columns.Count - 1; i++)
            {
                firstRow.Children.Add(new HtmlElement("th"));
                firstRow.GetChildrenByTagName("th")[firstRow.GetChildrenByTagName("th").Count - 1].Children.Add(new HtmlContent(Columns[i]));
            }
        }

        public void SetColumns(List<string> columnList)
        {
            this.Columns = columnList;
            CreateHeaderFromColumns();
        }

        public override string ToString()
        {
            return Root.ToString();
        }

        public HtmlElement GetLastRow()
        {
            return Rows[Rows.Count - 1];
        }

        public HtmlElement GetFirstRow()
        {
            return Rows[0];
        }
    }
}
