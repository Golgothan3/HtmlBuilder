using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace HtmlBuilder
{
    public class HtmlBuilderTable
    {
        public HtmlBuilderElement Root { get; } = new HtmlBuilderElement("table");
        public HtmlBuilderElement Header { get; } = new HtmlBuilderElement("thead");
        public HtmlBuilderElement Body { get; } = new HtmlBuilderElement("tbody");
        public HtmlBuilderElement Footer { get; } = new HtmlBuilderElement("tfoot");
        public List<HtmlBuilderElement> Rows { get; } = new List<HtmlBuilderElement>();
        public List<string> Columns { get; set; }

        public HtmlBuilderTable()
        {
            Root.Children.Add(Header);
            Root.Children.Add(Body);
            Root.Children.Add(Footer);
        }

        public void AddRows(bool addColumns, int rowsToAdd)
        {
            for (int i = 0; i <= rowsToAdd - 1; i++)
            {
                HtmlBuilderElement newRow = new HtmlBuilderElement("tr");
                Body.Children.Add(newRow);
                Rows.Add(newRow);
                if (addColumns)
                {
                    for (int j = 0; j <= Columns.Count - 1; j++)
                    {
                        newRow.Children.Add(new HtmlBuilderElement("td"));
                    }
                }
            }
        }

        public HtmlBuilderElement AddRow(List<string> columnData)
        {
            HtmlBuilderElement newRow = new HtmlBuilderElement("tr");
            Body.Children.Add(newRow);
            Rows.Add(newRow);

            for (int i = 0; i <= columnData.Count - 1; i++)
            {
                newRow.Children.Add(new HtmlBuilderElement("td"));
                newRow.GetChildrenByTagName("td").Last().Children.Add(new HtmlBuilderContent(columnData[i]));
            }

            return newRow;
        }

        public HtmlBuilderElement GetCellFromColumn(HtmlBuilderElement row, int columnIndex)
        {
            return row.Children[columnIndex];
        }

        public void CreateHeaderFromColumns()
        {
            Header.Children.Clear();
            Header.Children.Add(new HtmlBuilderElement("tr"));
            HtmlBuilderElement firstRow = Header.GetChildrenByTagName("tr")[0];
            for (int i = 0; i <= Columns.Count - 1; i++)
            {
                firstRow.Children.Add(new HtmlBuilderElement("th"));
                firstRow.GetChildrenByTagName("th").Last().Children.Add(new HtmlBuilderContent(Columns[i]));
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

        public HtmlBuilderElement GetLastRow()
        {
            return Rows.Last();
        }

        public HtmlBuilderElement GetFirstRow()
        {
            return Rows[0];
        }

        public void CreateFromDataTable(DataTable dt)
        {

            Columns = (from DataColumn column in dt.Columns select column.ColumnName).ToList<string>();
            CreateHeaderFromColumns();

            foreach (DataRow row in dt.Rows)
            {
                AddRow((from object value in row.ItemArray select value.ToString()).ToList<string>());
            }
        }
    }
}
