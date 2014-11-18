<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jobdetail.aspx.cs" Inherits="jobdetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
	</head>
	<body>
		<div data-role="page" data-theme="e" style="z-index:99999;">
			<div data-role="header" data-position="inline" data-theme="e">
        <img src="FCA/logo_FCA.png" />
			</div>			
  
		    <div data-role="content" data-theme="a">				
            <h2 id="jobTitle" runat="server">Job Title</h2>
            <br />
            <h4 id="jobLocation" runat="server">地点:</h4>
            <h4 id="jobVertical" runat="server">部门:</h4>
            <p id="jobDes" runat="server"></p>
            
            <form id="Form1" runat="server">
                <div class="ui-grid-a">			        
                    <div class="ui-block-a">
                    <asp:Button ID="applyBtn" class="ui-btn-check" runat="server" Text="申请职位" data-theme="a" onclick="applyBtn_Click" />
                    </div> 
                    <div class="ui-block-b">
                    <asp:Button ID="backBtn" class="ui-btn-check" 
                            runat="server" Text="返回" data-theme="a" onclick="backBtn_Click" />
                    </div>                    
                </div>

<div class="bdsharebuttonbox"><%--<a href="#" class="bds_more" data-cmd="more"></a><a title="分享到微信" href="#" class="bds_weixin" data-cmd="weixin"></a><a title="分享到新浪微博" href="#" class="bds_tsina" data-cmd="tsina"></a><a title="分享到人人网" href="#" class="bds_renren" data-cmd="renren"></a><a title="分享到腾讯微博" href="#" class="bds_tqq" data-cmd="tqq"></a><a title="分享到linkedin" href="#" class="bds_linkedin" data-cmd="linkedin"></a><a title="分享到Facebook" href="#" class="bds_fbook" data-cmd="fbook"></a><a title="分享到Twitter" href="#" class="bds_twi" data-cmd="twi"></a></div>
<script>    window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "", "bdMini": "2", "bdMiniList": false, "bdPic": "", "bdStyle": "0", "bdSize": "16" }, "share": {} }; with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];</script>--%>
            </form>     
           
            </div>
            
        </div>

         

	</body>
</html>
