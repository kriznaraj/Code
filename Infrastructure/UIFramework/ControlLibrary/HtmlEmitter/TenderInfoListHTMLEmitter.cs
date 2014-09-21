using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
namespace Controls.ControlLibrary
{
    internal class TenderInfoListHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private DenomControlPropertyBag _propertyBag;
        Dictionary<string, Dictionary<string, string>> _data;
        string _primaryID = string.Empty;
        string _currencySymbol = string.Empty;
        string _undefinedColumnName = string.Empty;
        string _undefinedColumnValue = string.Empty;
        List<string> _undefinedRowReadonlyComunsList = null;
        List<string> _undefinedRowEditableComunsList = null;
        #endregion

        #region "Constructors"

        public TenderInfoListHTMLEmitter(DenomDataSource dataSource, DenomControlPropertyBag propertyBag)
            : base(propertyBag.Validators, propertyBag.Mandatory)
        {
            this._propertyBag = propertyBag;
            this._data = dataSource != null ? dataSource.DataSource : null;
            this._undefinedColumnName = dataSource != null ? dataSource.UndefinedColumnName : string.Empty;
            this._undefinedColumnValue = dataSource != null ? dataSource.UndefinedColumnValue : string.Empty;
            this._primaryID = dataSource.PrimaryKey;
            if (propertyBag.UndefinedRowReadonlyColumns != null && propertyBag.UndefinedRowReadonlyColumns.Length > 0)
            {
                this._undefinedRowReadonlyComunsList = new List<string>(propertyBag.UndefinedRowReadonlyColumns.Split(','));
            }

            if (propertyBag.UndefinedRowEditableColumns != null && propertyBag.UndefinedRowEditableColumns.Length > 0)
            {
                this._undefinedRowEditableComunsList = new List<string>(propertyBag.UndefinedRowEditableColumns.Split(','));
            }
        }


        #endregion

        #region "Properties"

        #endregion

        #region "Implemented Methods"

        public override void Emit(out string controlHTMLString)
        {
            controlHTMLString = string.Empty;
            if (_propertyBag.Visibility)
            {
                //TagBuilder outerTable = new TagBuilder(TAG_DIV);
                //this.SetControlCssClasses(outerTable, this._propertyBag.CssClass, this._propertyBag.CssClass);
                TagBuilder outerTable = new TagBuilder(TAG_TABLE);
                this.SetCssClass(outerTable, _propertyBag.CssClass);
                this.SetAttribute(outerTable, ATTRIBUTE_ID, _propertyBag.ControlName.Replace(".", "_"));
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_GRANTTOTAL_REQUIRED, _propertyBag.GrantTotalRequired.ToString());
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_OTHERAMOUNT_REQUIRED, _propertyBag.OtherAmountRequired.ToString());
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_OTHERAMOUNT_LABEL, ControlLibraryConfig.ResourceService.GetLiteral(_propertyBag.OtherAmountLabelKey));
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_COLUMNS, GetColumnsAsString());
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_PRIMARYID, _primaryID);
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_UNDEFINED_COLUMN, _undefinedColumnName);
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_UNDEFINED_COLUMNVALUE, _undefinedColumnValue);
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_UNDEFINED_READONLY_COLUMNS, _propertyBag.UndefinedRowReadonlyColumns);
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_UNDEFINED_EDITABLE_COLUMNS, _propertyBag.UndefinedRowEditableColumns);
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_MOVEMENT_COLUMN, _propertyBag.MovementIndicatorColumn);
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_MOVEMENT_REQUIRED, _propertyBag.MovementIndicatorRequired.ToString());

                this.SetAttribute(outerTable, ATTRIBUTE_DATA_DENOMMODE, _propertyBag.DenomMode);
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_OTHERAMOUNT_POSITION, _propertyBag.OtherAmountPosition.ToString());

                this.SetAttribute(outerTable, ATTRIBUTE_DATA_ACTIONURL, _propertyBag.ActionUrl);
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_ISVIEWMODE, _propertyBag.IsViewMode.ToString());
                this.SetAttribute(outerTable, ATTRIBUTE_DATA_VALIDATIONMESSAGE, ControlLibraryConfig.ResourceService.GetLiteral(_propertyBag.ValidationMessageKey));
                if (_data != null && _data.Count > 0)
                    this.SetAttribute(outerTable, ATTRIBUTE_DATA_CONTAINSROW, _data.Count);
                else
                    this.SetAttribute(outerTable, ATTRIBUTE_DATA_CONTAINSROW, "0");
                _currencySymbol = ControlLibraryConfig.ResourceService.GetCurrencySymbol();

                this.SetAttribute(outerTable, ATTRIBUTE_DATA_CURRENCYSYMBOL, _currencySymbol);
                if (_propertyBag.DenomTemplate != null)
                {
                    this.SetInnerHtml(outerTable, BuildHeader());
                    this.SetInnerHtml(outerTable, BuildBody(), true);

                    if (_propertyBag.GrantTotalRequired)
                    {
                        this.SetInnerHtml(outerTable, BuildFooter(), true);
                    }
                }

                controlHTMLString = outerTable.ToString() + GetErrorLabel(this._propertyBag) + BuildScript();
            }
        }

        #endregion

        #region "Private Methods"

        private string BuildHeader()
        {
            TagBuilder head = new TagBuilder(TAG_THEAD);
            TagBuilder tr = new TagBuilder(TAG_TR);
            this.SetCssClass(tr, _propertyBag.HeaderCssClass);
            int columnCount = 0;
            foreach (DenomTemplateColumnDefinition column in _propertyBag.DenomTemplate.DenomTemplateColumnDefinition)
            {
                TagBuilder th = new TagBuilder(TAG_TH);
                this.SetAttribute(th, ATTRIBUTE_ID, string.Format("{0}_hdr_{1}", _propertyBag.ControlName.Replace(".", "_"), column.DisplayMember));
                this.SetAttribute(th, ATTRIBUTE_DATA_COLUMNNAME, column.DisplayMember);
                this.SetAttribute(th, ATTRIBUTE_DATA_TOTALREQUIRED, column.TotalRequired);
                this.SetAttribute(th, ATTRIBUTE_DATA_CALCULATIONREQUIRED, column.CalculationRequired);
                this.SetAttribute(th, ATTRIBUTE_DATA_FORMULA, column.Formula);
                this.SetAttribute(th, ATTRIBUTE_DATA_ALLOWDECIMAL, column.AllowDecimal);
                this.SetAttribute(th, ATTRIBUTE_DATA_ALLOWNEGATIVE, column.AllowNegativeValue);
                this.SetAttribute(th, ATTRIBUTE_DATA_FORMULA, column.Formula);
                this.SetAttribute(th, ATTRIBUTE_DATA_SEED, column.Seed);
                this.SetAttribute(th, ATTRIBUTE_DATA_COLUMNDATATYPE, column.ColumnDataType.ToString());
                this.SetAttribute(th, ATTRIBUTE_DATA_ISREADONLY, column.IsReadOnly.ToString());
                this.SetAttribute(th, ATTRIBUTE_DATA_ISVISIBLE, column.IsVisible);
                this.SetAttribute(th, ATTRIBUTE_DATA_ISEDITABLE, column.IsEditable);
                this.SetAttribute(th, ATTRIBUTE_DATA_SPINNERREQUIRED, column.SpinnerRequired);
                this.SetAttribute(th, ATTRIBUTE_DATA_DECIMALPLACES, column.DecimalPlaces);
                this.SetAttribute(th, ATTRIBUTE_DATA_BINDINGMODE, column.BindingType.ToString());
                this.SetAttribute(th, ATTRIBUTE_DATA_ALIGNMENT, column.Alignment);


                if (column.IsEditable)
                    this.SetAttribute(th, ATTRIBUTE_DATA_DATATYPE, DENOM_FIELD_TYPE_INPUT);
                else
                    this.SetAttribute(th, ATTRIBUTE_DATA_DATATYPE, DENOM_FIELD_TYPE_TEXT);

                if (column.IsVisible)
                {
                    if (column.CurrencySymbolRequired && (column.CurrencySymbolPosition == CurrencySymbolPositionType.Header || column.CurrencySymbolPosition == CurrencySymbolPositionType.HeaderAndCell || column.CurrencySymbolPosition == CurrencySymbolPositionType.HeaderAndFooter || column.CurrencySymbolPosition == CurrencySymbolPositionType.HeaderAndCellAndFooter))
                        this.SetInnerHtml(th, string.Format("{0} ({1})", ControlLibraryConfig.ResourceService.GetLiteral(column.HeaderName), _currencySymbol));
                    else
                        this.SetInnerHtml(th, ControlLibraryConfig.ResourceService.GetLiteral(column.HeaderName));
                }
                else
                {
                    this.SetAttribute(th, ATTRIBUTE_STYLE, "display:none");
                    this.SetInnerHtml(th, string.Empty);
                }
                if (columnCount == 0 && column.IsVisible)
                {
                    TagBuilder hdnValidation = new TagBuilder(TAG_INPUT);
                    this.SetAttribute(hdnValidation, ATTRIBUTE_ID, string.Format("hdn_{0}_validation", _propertyBag.ControlName.Replace(".", "_")));
                    this.SetAttribute(hdnValidation, ATTRIBUTE_TYPE, ATTR_VAL_HIDDEN);
                    this.SetInnerHtml(th, hdnValidation.ToString(), true);
                    columnCount += 1;
                }

                this.SetInnerHtml(tr, th.ToString(), true);
            }
            if (_propertyBag.MovementIndicatorRequired)
            {
                TagBuilder movementTh = new TagBuilder(TAG_TH);
                this.SetInnerHtml(movementTh, string.Empty);
                this.SetAttribute(movementTh, ATTRIBUTE_DATA_INDICATOR, "yes");
                this.SetInnerHtml(tr, movementTh.ToString(), true);
            }
            this.SetInnerHtml(head, tr.ToString());

            return head.ToString();
        }

        private string BuildBody()
        {
            TagBuilder body = new TagBuilder(TAG_TBODY);
            this.SetAttribute(body, ATTRIBUTE_ID, string.Format("{0}_tbody", _propertyBag.ControlName.Replace(".", "_")));
            StringBuilder undefinedtr = new StringBuilder();
            StringBuilder normalRows = new StringBuilder();
            string rowColumnValue = string.Empty;
            if (_data != null && _data.Count > 0)
            {
                int rowCount = 0;
                bool isUndefinedRow = false;
                bool undefinedAdded = false;
                foreach (IDictionary<string, string> row in _data.Values)
                {
                    //Undefined tender code
                    if (!string.IsNullOrEmpty(this._undefinedColumnName))
                    {
                        if (row.ContainsKey(this._undefinedColumnName))
                        {
                            if (row[this._undefinedColumnName] == this._undefinedColumnValue)
                            {
                                isUndefinedRow = true;
                            }
                        }
                    }

                    //***********************

                    TagBuilder tr = new TagBuilder(TAG_TR);
                    this.SetAttribute(tr, ATTRIBUTE_ID, string.Format("{0}_tr_{1}", _propertyBag.ControlName.Replace(".", "_"), row[_primaryID]));
                    bool isFirstChild = true;
                    foreach (DenomTemplateColumnDefinition column in _propertyBag.DenomTemplate.DenomTemplateColumnDefinition)
                    {
                        if (row.ContainsKey(column.DisplayMember))
                        {
                            rowColumnValue = row[column.DisplayMember];
                        }
                        else
                        {
                            rowColumnValue = string.Empty;
                        }

                        TagBuilder td = new TagBuilder(TAG_TD);
                        TagBuilder tdContainerDiv = new TagBuilder(TAG_DIV);
                        TagBuilder spinMasterDiv = new TagBuilder(TAG_DIV);
                        this.SetCssClass(spinMasterDiv, "action-increment-decrement");
                        string textName = string.Format("{0}_{1}_{2}", _propertyBag.ControlName.Replace(".", "_"), column.DisplayMember, row[_primaryID]);
                        string Name = string.Format("{0}[{1}].{2}", _propertyBag.ControlName, rowCount, column.DisplayMember);
                        if (column.IsVisible)
                        {
                            if (column.IsEditable || this.IsUndefinedEditable(isUndefinedRow, column.DisplayMember))
                            {
                                TagBuilder textBox = new TagBuilder(TAG_INPUT);
                                if (column.Alignment.ToLower() == "right")
                                    this.SetCssClass(textBox, "tender-curreny-right-align");

                                if (column.Alignment.ToLower() == "left")
                                    this.SetCssClass(textBox, "tender-curreny-left-align");

                                TagBuilder hdnTextBox = new TagBuilder(TAG_INPUT);
                                this.SetAttribute(hdnTextBox, ATTRIBUTE_TYPE, ATTR_VAL_HIDDEN);


                                this.SetAttribute(textBox, ATTRIBUTE_TYPE, ATTR_VAL_TEXT);
                                if (!_propertyBag.IsViewMode)
                                {
                                    this.SetAttribute(textBox, ATTRIBUTE_ONKEYUP, "_" + _propertyBag.ControlName.Replace(".", "_") + ".denomTextInputKeyUp('" + textName + "','" + column.DisplayMember + "')");
                                    this.SetAttribute(textBox, ATTRIBUTE_ONKEYPRESS, "_" + _propertyBag.ControlName.Replace(".", "_") + ".AcceptNumberOnly(event)");
                                    this.SetAttribute(textBox, ATTRIBUTE_ONFOCUS, "_" + _propertyBag.ControlName.Replace(".", "_") + ".OnInputFieldGotFocus('" + row[_primaryID] + "')");
                                    this.SetAttribute(textBox, ATTRIBUTE_ONLEAVE, "_" + _propertyBag.ControlName.Replace(".", "_") + ".OnInputFieldLostFocus('" + row[_primaryID] + "')");
                                }
                                this.SetAttribute(textBox, ATTRIBUTE_DATA_COLUMNNAME, column.DisplayMember);
                                this.SetAttribute(textBox, ATTRIBUTE_DATA_PRIMARY_ID, row[_primaryID]);

                                this.SetCssClass(textBox, "krishna-textbox");
                                this.SetAttribute(textBox, ATTRIBUTE_ID, textName);
                                this.SetAttribute(hdnTextBox, ATTRIBUTE_ID, string.Format("hdn_{0}", textName));

                                this.SetAttribute(textBox, ATTRIBUTE_NAME, Name);
                                
                                this.SetAttribute(textBox, ATTRIBUTE_VALUE, rowColumnValue);

                                this.SetAttribute(hdnTextBox, ATTRIBUTE_VALUE, rowColumnValue);

                                if ((column.IsReadOnly || _propertyBag.IsViewMode || this.IsUndefinedReadonly(isUndefinedRow, column.DisplayMember)) &&!this.IsUndefinedEditable(isUndefinedRow ,column.DisplayMember) )
                                {
                                    this.SetAttribute(textBox, ATTR_VAL_READONLY, true);
                                    this.SetCssClass(textBox, "krishna-denom-datagrid-text-readonly");
                                }
                                
                                if (this.IsUndefinedReadonly(isUndefinedRow, column.DisplayMember) || this.IsUndefinedEditable(isUndefinedRow, column.DisplayMember))
                                {
                                    this.SetAttribute(textBox, ATTRIBUTE_DATA_SKIP_STATECHANGE, true);
                                }
                                else
                                {
                                    this.SetAttribute(textBox, ATTRIBUTE_DATA_SKIP_STATECHANGE, false);
                                }
                                this.SetCssClass(td, "bg-none");
                                this.SetInnerHtml(tdContainerDiv, textBox.ToString());
                                this.SetInnerHtml(tdContainerDiv, hdnTextBox.ToString(), true);

                                if (column.SpinnerRequired && !_propertyBag.IsViewMode && !isUndefinedRow)
                                {
                                    //Decrement Button Div
                                    TagBuilder divDecrement = new TagBuilder(TAG_DIV);
                                    string btnSpinnerMinusName = string.Format("{0}_btnSpinnerminus_{1}", _propertyBag.ControlName.Replace(".", "_"), column.DisplayMember);
                                    TagBuilder btnMinusTag = new TagBuilder(TAG_INPUT);
                                    this.SetAttribute(btnMinusTag, ATTRIBUTE_TYPE, TAG_BUTTON);
                                    this.SetAttribute(btnMinusTag, ATTRIBUTE_VALUE, "-");
                                    this.SetAttribute(btnMinusTag, ATTRIBUTE_ONCLICK, "_" + _propertyBag.ControlName.Replace(".", "_") + ".spinnerClick('-','" + column.DisplayMember + "'," + row[_primaryID] + ")");
                                    this.SetAttribute(btnMinusTag, ATTRIBUTE_ID, btnSpinnerMinusName);
                                    this.SetCssClass(btnMinusTag, "inventory-spin");
                                    this.SetInnerHtml(divDecrement, btnMinusTag.ToString());
                                    this.SetInnerHtml(spinMasterDiv, divDecrement.ToString(), true);

                                    //Divider Div
                                    TagBuilder divDivider = new TagBuilder(TAG_DIV);
                                    this.SetCssClass(divDivider, "krishna-datagrid-denom-amount-strip");
                                    this.SetInnerHtml(spinMasterDiv, divDivider.ToString(), true);

                                    //Increment Button Div
                                    TagBuilder divIncrement = new TagBuilder(TAG_DIV);
                                    string btnSpinnerPlusName = string.Format("{0}_btnSpinnerplus_{1}", _propertyBag.ControlName.Replace(".", "_"), column.DisplayMember);
                                    TagBuilder btnPlusTag = new TagBuilder(TAG_INPUT);
                                    this.SetAttribute(btnPlusTag, ATTRIBUTE_TYPE, TAG_BUTTON);
                                    this.SetAttribute(btnPlusTag, ATTRIBUTE_VALUE, "+");
                                    this.SetAttribute(btnPlusTag, ATTRIBUTE_ONCLICK, "_" + _propertyBag.ControlName.Replace(".", "_") + ".spinnerClick('+','" + column.DisplayMember + "'," + row[_primaryID] + ")");
                                    this.SetAttribute(btnPlusTag, ATTRIBUTE_ID, btnSpinnerPlusName);
                                    this.SetCssClass(btnPlusTag, "inventory-spin");
                                    this.SetInnerHtml(divIncrement, btnPlusTag.ToString());
                                    this.SetInnerHtml(spinMasterDiv, divIncrement.ToString(), true);
                                    this.SetInnerHtml(tdContainerDiv, spinMasterDiv.ToString(), true);
                                }

                                this.SetInnerHtml(td, tdContainerDiv.ToString(), true);
                            }
                            else
                            {
                                if (column.BindingType == BindingMode.TwoWay)
                                {
                                    TagBuilder bindingHidden = new TagBuilder(TAG_INPUT);
                                    this.SetAttribute(bindingHidden, ATTRIBUTE_TYPE, ATTR_VAL_HIDDEN);
                                    this.SetAttribute(bindingHidden, ATTRIBUTE_VALUE, rowColumnValue);
                                    this.SetAttribute(bindingHidden, ATTRIBUTE_ID, textName);
                                    this.SetAttribute(bindingHidden, ATTRIBUTE_NAME, Name);
                                    this.SetAttribute(bindingHidden, ATTRIBUTE_DATA_COLUMNNAME, column.DisplayMember);
                                    this.SetAttribute(td, ATTRIBUTE_ID, string.Format("td_{0}", textName));
                                    this.SetAttribute(td, ATTRIBUTE_NAME, string.Format("td.{0}", Name));
                                    this.SetInnerHtml(td, bindingHidden.ToString());
                                }
                                else
                                {
                                    this.SetAttribute(td, ATTRIBUTE_DATA_COLUMNNAME, column.DisplayMember);
                                    this.SetAttribute(td, ATTRIBUTE_ID, textName);
                                    this.SetAttribute(td, ATTRIBUTE_NAME, Name);
                                }




                                this.SetInnerHtml(td, rowColumnValue, true);
                                if (column.Alignment.ToLower() == "right")
                                {
                                    this.SetCssClass(td, "tender-curreny-right-align");
                                }

                                if (column.Alignment.ToLower() == "left")
                                {
                                    this.SetCssClass(td, "tender-curreny-left-align");
                                }

                                if (isFirstChild)
                                {
                                    if (!isUndefinedRow)
                                    {
                                        if (_propertyBag.IsViewMode)
                                            this.SetCssClass(td, "krishna-datagrid-denom-child-read-only");
                                        else
                                            this.SetCssClass(td, "krishna-datagrid-denom-child");
                                    }
                                    isFirstChild = false;
                                }

                                if (isUndefinedRow && !undefinedAdded)
                                {
                                    this.SetCssClass(td, "krishna-datagrid-denom-undefined-tender");
                                    undefinedAdded = true;
                                }
                            }
                        }
                        else
                        {
                            if (column.BindingType == BindingMode.TwoWay)
                            {
                                TagBuilder bindingHidden = new TagBuilder(TAG_INPUT);
                                this.SetAttribute(bindingHidden, ATTRIBUTE_TYPE, ATTR_VAL_HIDDEN);
                                this.SetAttribute(bindingHidden, ATTRIBUTE_VALUE, rowColumnValue);
                                this.SetAttribute(bindingHidden, ATTRIBUTE_ID, textName);
                                this.SetAttribute(bindingHidden, ATTRIBUTE_NAME, Name);
                                this.SetAttribute(bindingHidden, ATTRIBUTE_DATA_COLUMNNAME, column.DisplayMember);
                                this.SetAttribute(td, ATTRIBUTE_ID, string.Format("td_{0}", textName));
                                this.SetAttribute(td, ATTRIBUTE_NAME, string.Format("td.{0}", Name));
                                this.SetInnerHtml(td, bindingHidden.ToString());
                            }
                            else
                            {
                                this.SetAttribute(td, ATTRIBUTE_DATA_COLUMNNAME, column.DisplayMember);
                                this.SetAttribute(td, ATTRIBUTE_ID, textName);
                                this.SetAttribute(td, ATTRIBUTE_NAME, Name);
                            }
                            this.SetAttribute(td, ATTRIBUTE_STYLE, "display:none");


                            this.SetInnerHtml(td, rowColumnValue, true);
                        }
                        this.SetInnerHtml(tr, td.ToString(), true);

                    }
                    if (_propertyBag.MovementIndicatorRequired)
                    {
                        TagBuilder mvementDiv = new TagBuilder(TAG_DIV);
                        TagBuilder movementTd = new TagBuilder(TAG_TD);
                        this.SetCssClass(movementTd, "movement-indictaor-bg");
                        //this.SetInnerHtml(movementTd, string.Empty);
                        if (!string.IsNullOrEmpty(_propertyBag.MovementIndicatorColumn))
                        {
                            if (row.Keys.Contains(_propertyBag.MovementIndicatorColumn))
                            {
                                if(row[_propertyBag.MovementIndicatorColumn] == "In")
                                    this.SetCssClass(mvementDiv, "movement-indicator-in");
                                else if (row[_propertyBag.MovementIndicatorColumn] == "Out")
                                    this.SetCssClass(mvementDiv, "movement-indicator-out");
                            }
                        }
                        this.SetAttribute(movementTd, ATTRIBUTE_DATA_INDICATOR, "yes");
                        this.SetInnerHtml(movementTd, mvementDiv.ToString());
                        this.SetInnerHtml(tr, movementTd.ToString(), true);
                    }

                    if (!isUndefinedRow)
                    {
                        normalRows.Append(tr.ToString());
                    }
                    else
                    {
                        undefinedtr.Append(tr.ToString());
                        isUndefinedRow = false;
                    }
                    rowCount += 1;
                }
            }
            string finalBody = string.Empty;
            if (_propertyBag.DenomMode == DenomModeType.Full)
            {
                if (this._propertyBag.OtherAmountPosition == PositionType.Top)
                {
                    finalBody = undefinedtr.ToString() + normalRows.ToString();
                    this.SetInnerHtml(body, finalBody, true);
                }
                else
                {
                    finalBody = normalRows.ToString() + undefinedtr.ToString();
                    this.SetInnerHtml(body, finalBody, true);
                }
            }

            if (_propertyBag.DenomMode == DenomModeType.Denom)
            {
                finalBody = normalRows.ToString();
                this.SetInnerHtml(body, finalBody, true);
            }

            if (_propertyBag.DenomMode == DenomModeType.Undefined)
            {
                finalBody = undefinedtr.ToString();
                this.SetInnerHtml(body, finalBody, true);
            }
            return body.ToString();
        }

        private string BuildFooter()
        {
            TagBuilder footer = new TagBuilder(TAG_TFOOT);
            TagBuilder tr = new TagBuilder(TAG_TR);
            this.SetCssClass(tr, _propertyBag.FooterCssClass);
            foreach (DenomTemplateColumnDefinition column in _propertyBag.DenomTemplate.DenomTemplateColumnDefinition)
            {

                TagBuilder td = new TagBuilder(TAG_TD);
                if (column.Alignment.ToLower() == "right")
                    this.SetCssClass(td, "tender-curreny-right-align");

                if (column.Alignment.ToLower() == "left")
                    this.SetCssClass(td, "tender-curreny-left-align");

                if (!column.IsVisible)
                    this.SetAttribute(td, ATTRIBUTE_STYLE, "display:none");

                if (!string.IsNullOrEmpty(column.FooterText))
                {
                    this.SetInnerHtml(td, ControlLibraryConfig.ResourceService.GetLiteral(column.FooterText));
                }
                else
                {
                    if (column.ColumnDataType == DenomColumnDataType.Number && column.TotalRequired)
                    {
                        this.SetAttribute(td, ATTRIBUTE_ID, string.Format("{0}_gt_{1}", _propertyBag.ControlName.Replace(".", "_"), column.DisplayMember));
                        this.SetInnerHtml(td, "0");
                    }
                    else
                    {
                        this.SetInnerHtml(td, string.Empty);
                    }
                }

                this.SetInnerHtml(tr, td.ToString(), true);

            }
            if (_propertyBag.MovementIndicatorRequired)
            {
                TagBuilder movementTd = new TagBuilder(TAG_TD);
                this.SetInnerHtml(movementTd, string.Empty);
                this.SetAttribute(movementTd, ATTRIBUTE_DATA_INDICATOR, "yes");
                this.SetInnerHtml(tr, movementTd.ToString(), true);
            }
            this.SetInnerHtml(footer, tr.ToString(), true);

            return footer.ToString();
        }

        private string BuildScript()
        {
            int rowCount = 0;
            TagBuilder ScriptTag = new TagBuilder(TAG_SCRIPT);
            this.SetAttribute(ScriptTag, ATTRIBUTE_LANG, SCRIPT_NAME);
            if (_data != null && _data.Count > 0)
            {
                rowCount = _data.Count;
            }
            StringBuilder script = new StringBuilder();
            script.Append("var _" + _propertyBag.ControlName.Replace(".", "_") + "= new BallyTenderInfoList('" + _propertyBag.ControlName.Replace(".", "_") + "');");
            script.Append("$(document).ready(function () { _" + _propertyBag.ControlName.Replace(".", "_") + ".Init(); _" + _propertyBag.ControlName.Replace(".", "_") + ".setLayout('" + rowCount + "');});");
            return GetScriptString(ScriptTag, script);
        }

        private string GetColumnsAsString()
        {
            StringBuilder columnResult = new StringBuilder();
            foreach (DenomTemplateColumnDefinition column in _propertyBag.DenomTemplate.DenomTemplateColumnDefinition)
            {
                if (columnResult.ToString() == string.Empty)
                    columnResult.Append(column.DisplayMember);
                else
                    columnResult.Append("|" + column.DisplayMember);
            }
            return columnResult.ToString();
        }

        private string GetZeroString(int zeroCount)
        {
            string zeroString = string.Empty;
            if (zeroCount > 0)
            {
                for (int count = 1; count <= zeroCount; count++)
                {
                    zeroString = zeroString + "0";
                }
            }
            return zeroString;
        }

        private string GetDecimalValue(string rowColumnValue, bool allowDecimal, int decimalPlaces)
        {
            if (!string.IsNullOrEmpty(rowColumnValue))
            {
                if (allowDecimal)
                {
                    if (rowColumnValue.Contains("."))
                    {
                        var decimalString = rowColumnValue.Substring(rowColumnValue.IndexOf('.'), rowColumnValue.Length - rowColumnValue.IndexOf('.'));
                        if (decimalString.Length != decimalPlaces)
                        {
                            if (decimalString.Length > decimalPlaces)
                            {
                                rowColumnValue = rowColumnValue.Substring(0, (rowColumnValue.Length - (decimalString.Length - decimalPlaces)));
                            }
                            else
                            {
                                rowColumnValue = rowColumnValue + GetZeroString(decimalString.Length);
                            }
                        }
                    }
                    else
                    {
                        rowColumnValue = rowColumnValue + "." + GetZeroString(decimalPlaces);
                    }
                }
                else
                {
                    rowColumnValue = rowColumnValue.Substring(0, rowColumnValue.IndexOf('.') - 1);
                }
            }
            else
            {
                rowColumnValue = "0." + GetZeroString(decimalPlaces);
            }

            return rowColumnValue;
        }

        private bool IsUndefinedReadonly(bool isUndefinedRow, string displayMember)
        {
            if (isUndefinedRow)
            {
                if (_undefinedRowReadonlyComunsList != null && _undefinedRowReadonlyComunsList.Contains(displayMember))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsUndefinedEditable(bool isUndefinedRow, string displayMember)
        {
            if (isUndefinedRow)
            {
                if ((_undefinedRowEditableComunsList != null && _undefinedRowEditableComunsList.Contains(displayMember)))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}
