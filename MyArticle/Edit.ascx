﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="MyArticle.Edit" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web.Deprecated" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>


<script src="/DesktopModules\MyArticle\MyArticleScript.js" type="text/javascript"></script>

<table style="border-collapse: separate; border-spacing: 10px;">
    <tr>
        <td>
            <dx:ASPxLabel ID="Title_ASPxLabel" runat="server"  Theme="Youthful">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox ID="Title_ASPxTextBox" MaxLength="64" runat="server" Width="300px" Theme="Youthful">
            </dx:ASPxTextBox>
        </td>

    </tr>

    <tr>
        <td>
            <dx:ASPxLabel ID="Author_ASPxLabel" runat="server"  Theme="Youthful">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox ID="Author_ASPxTextBox" MaxLength="64" runat="server" Width="300px" Theme="Youthful">
            </dx:ASPxTextBox>
        </td>

    </tr>

       <tr>
        <td>
            <dx:ASPxLabel ID="Tag_ASPxLabel" runat="server"  Theme="Youthful">
            </dx:ASPxLabel>
        </td>
        <td>
           <dnn:TermsSelector runat="server" ID="Terms_TermsSelector" ></dnn:TermsSelector>
        </td>

    </tr>

    <tr>
        <td>
            <dx:ASPxLabel ID="IsPublished_ASPxLabel" runat="server"  Theme="Youthful">
            </dx:ASPxLabel>

        </td>
        <td>
            <dx:ASPxCheckBox ID="IsPublished_ASPxCheckBox" runat="server" Theme="Youthful"></dx:ASPxCheckBox>
        </td>

    </tr>

    <tr>
        <td>
            <dx:ASPxLabel ID="IsComment_ASPxLabel" runat="server"  Theme="Youthful">
            </dx:ASPxLabel>

        </td>
        <td>
            <dx:ASPxCheckBox ID="IsComment_ASPxCheckBox" runat="server" Theme="Youthful"></dx:ASPxCheckBox>
        </td>

    </tr>

    <tr>
        <td>
            <dx:ASPxLabel ID="Description_ASPxLabel" runat="server"  Theme="Youthful">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox  ID="Description_ASPxTextBox" runat="server" Width="300px" EnableTheming="True" Theme="Youthful" MaxLength="60px">
               
            </dx:ASPxTextBox>
        </td>

    </tr>

    <tr>
        <td>
            <dx:ASPxLabel ID="Thumbnail_ASPxLabel" runat="server"  Theme="Youthful"></dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxHiddenField ID="Thumbnail_ASPxHiddenField"  ClientInstanceName="Thumbnail_ASPxHiddenField" runat="server"></dx:ASPxHiddenField>
            <dx:ASPxImage   ID="Thumbnail_ASPxImage" ClientInstanceName="Thumbnail_ASPxImage" Width="100px" Height="100px" runat="server"  Cursor="pointer">
                <ClientSideEvents Click="function(s, e) {
	         var win = Thumbnail_ASPxPopupControl.windows[0];
             Thumbnail_ASPxPopupControl.ShowWindow(win, 0);
}" />

            </dx:ASPxImage>
        </td>
    </tr>

    <tr>
        <td>
            <dx:ASPxLabel ID="Body_ASPxLabel" runat="server" Text="Body" Theme="Youthful">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxHtmlEditor ID="Body_ASPxHtmlEditor" runat="server" EnableTheming="True" Theme="Youthful" Height="556px" Width="1098px">
            </dx:ASPxHtmlEditor>
        </td>

    </tr>


    <tr>
        <td></td>
        <td>
            <dx:ASPxButton AutoPostBack="false" Height="30px" Width="80px" ID="Save_ASPxButton" runat="server" Text="Save"  Theme="Youthful">
                <ClientSideEvents Click="function(s, e) {
	SaveArticle_ASPxCallback.PerformCallback('');
}" />
            </dx:ASPxButton>
            <dx:ASPxButton Height="30px" Width="80px" ID="Cancel_ASPxButton" runat="server" Text="Cancel" OnClick="Cancel_ASPxButton_Click" Theme="Youthful"></dx:ASPxButton>
        </td>
    </tr>
</table>




<dx:ASPxPopupControl ClientInstanceName="Thumbnail_ASPxPopupControl" ID="Thumbnail_ASPxPopupControl" runat="server" Theme="Youthful" >
    <Windows>
        <dx:PopupWindow>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxFileManager OnCustomCallback="Thumbnail_ASPxFileManager_CustomCallback" ClientInstanceName="Thumbnail_ASPxFileManager" ID="Thumbnail_ASPxFileManager" runat="server" EnableTheming="True" Theme="Youthful">
                        <SettingsToolbar>
                            <Items>
                                <dx:FileManagerToolbarCustomButton CommandName="Select" Text="Select">
                                </dx:FileManagerToolbarCustomButton>
                            </Items>
                        </SettingsToolbar>
                      
                     
                        <ClientSideEvents CustomCommand="function(s, e) { 
                                                   
                             var win = Thumbnail_ASPxPopupControl.windows[0];
                             Thumbnail_ASPxPopupControl.HideWindow(win, 0);
                             Thumbnail_ASPxFileManager.PerformCallback();
                            
	                         }" EndCallback="function(s, e) {
                             Thumbnail_ASPxImage.SetImageUrl(s.cpResult);
                             Thumbnail_ASPxHiddenField.Add('ImageUrl', s.cpResult)
                            
	                         }" />
                        <SettingsContextMenu>
                            <Items>
                                <dx:FileManagerToolbarCustomButton Text="Select">
                                </dx:FileManagerToolbarCustomButton>
                            </Items>
                        </SettingsContextMenu>
                    </dx:ASPxFileManager>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:PopupWindow>
    </Windows>
</dx:ASPxPopupControl>


<dx:ASPxCallback  ID="SaveArticle_ASPxCallback" runat="server" ClientInstanceName="SaveArticle_ASPxCallback" OnCallback="SaveArticle_ASPxCallback_Callback" >
    <ClientSideEvents CallbackComplete="function(s, e) {
	if(e.result == 'Err')
        {
        alert(s.cpMsg);
        }
}" />
</dx:ASPxCallback>


<script type="text/javascript">

    function ValidateArticleInfo(title, author, description, body)
    {
        if(title.length < 1 || title.length > 32)
        {
            alert("title error")
        }
        else if(author.length < 1 || author.length > 16)
        {
            aler("author error")
        }
        else if (description.length < 1 || description.length > 128) {
            aler("description error")
        }
        else if (body.length < 1) {
            aler("body error")
        }
    }

</script>