<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajoblist.aspx.cs" Inherits="ajoblistaspx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!doctype html>
<html>
	<head>
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width,initial-scale=1">
		<title>FCA</title>
		<link rel="stylesheet" href="themes/Bootstrap.css">
		<link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.0/jquery.mobile.structure-1.4.0.min.css" />
		<link rel="stylesheet" href="themes/jquery.mobile.icons.min.css" />
		<script src="http://code.jquery.com/jquery-1.8.2.min.js" type="text/javascript"></script>
		<script src="http://code.jquery.com/mobile/1.4.0/jquery.mobile-1.4.0.min.js" type="text/javascript"></script>
        <style>
        .ui-block-b .ui-btn{        
        font-size:0.86em;
        }
        </style>
	</head>
	<body>
		<div data-role="page" data-theme="e" style="z-index:99999">
			<div data-role="header" data-position="inline" data-theme="e">
        <img src="FCA/logo_FCA.png" />
			</div>
			
            <div data-position="inline" data-role="content" role="banner" class="ui-content" style="background:#fff;border:0">
            <form runat="server">
			        <fieldset class="ui-grid-a">			        
                    <div class="ui-block-a">
                    <input type="text" name="name" id="SearchBox" runat="server" value="" style="padding-top: 4px; padding-bottom: 4px;" />
                    </div> 
                    <div class="ui-block-b">
                    <fieldset class="ui-grid-a">
                    <div class="ui-block-a">	
                    <asp:Button ID="searchBtn" runat="server"  Text="搜索" data-theme="a" onclick="searchBtn_Click" />    
                    </div>     
                    <div class="ui-block-b">
                    <asp:Button ID="adSearch" runat="server"
                             Text="高级" data-theme="a" 
                            onclick="adSearch_Click" />  
                    </div>   
                    </fieldset>     
                    </div>
                </fieldset>
                    
<%--                    <a data-icon="check" href="adsearch.aspx" class="ui-link ui-btn ui-btn-a ui-icon-search ui-btn-icon-left ui-shadow ui-corner-all" data-role="button" role="button">高级搜索</a>--%>
                    
            </form>
            </div>          
		    <div data-role="content" data-theme="a">				
            
            <div id="hbJobContent" runat="server">
				<ul data-role="listview" data-inset="true" data-divider-theme="a">
					<li data-role="list-divider">Swatch A</li>
					<li data-icon="gear"><a href="">A list item</a></li>
				</ul>

				<ul data-role="listview" data-inset="true" data-divider-theme="b">
					<li data-role="list-divider">Swatch B</li>
					<li data-icon="info"><a href="">A list item</a></li>
				</ul>

				<ul data-role="listview" data-inset="true" data-divider-theme="c">
					<li data-role="list-divider">Swatch C</li>
					<li data-icon="check"><a href="">A list item</a></li>
				</ul>

				<ul data-role="listview" data-inset="true" data-divider-theme="d">
					<li data-role="list-divider">Swatch D</li>
					<li data-icon="grid"><a href="">A list item</a></li>
				</ul>

				<ul data-role="listview" data-inset="true" data-divider-theme="e">
					<li data-role="list-divider">Swatch E</li>
					<li data-icon="alert"><a href="">A list item</a></li>
				</ul>

				<ul data-role="listview" data-inset="true" data-divider-theme="f">
					<li data-role="list-divider">Swatch F</li>
					<li data-icon="refresh"><a href="">A list item</a></li>
				</ul>
            </div>
			</div>
            <div data-theme="e" data-role="footer" role="contentinfo" class="ui-footer ui-bar-a" style="text-align:center">
            <a data-iconpos="right" data-icon="arrow-l" data-role="button" href="" class="ui-link ui-btn ui-icon-arrow-l ui-btn-icon-right ui-shadow ui-corner-all" role="button" id="prePage" runat="server">上一页</a> <span id="pageIndex" runat="server">1/40</span>
            <a data-iconpos="right" data-icon="arrow-r" data-role="button" href="" class="ui-link ui-btn ui-icon-arrow-r ui-btn-icon-right ui-shadow ui-corner-all" role="button" id="nextPage" runat="server">下一页</a>
            </div>
        </div>
	</body>
</html>
