<%@ Page Language="C#" AutoEventWireup="true" CodeFile="details.aspx.cs" Inherits="details" %>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="utf-8" />
  <title>HR Boss - Job Detail</title>
  <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0" /> 
  <link rel="stylesheet" media="all" href="style.css" type="text/css">
  <script type="text/javascript" src="http://platform.linkedin.com/in.js">
    api_key:759wbpflbgm7wc
</script>
</head>

<body>
<div class="wrap">
    <header id="header" style="">
      <div class="logo"><a href="index.aspx"><img src="images/logo.png" alt="Gallert Mobi - Free mobile website templates [Mobifreaks]"/></a></div>
      <div class="options">
      	<ul>
      		<li>Menu</li>
      	</ul>
      </div>
      <div class="clear"></div>

	    <nav class="vertical menu">
	    	<ul>
	            <li><a href="index.aspx">Home</a></li>
	            <li><a href="search.aspx">Search</a></li>
        	</ul>
	    </nav>
    </header>
    
        
    <div class="content" runat="server">
    	<article>
				<h1 id="jobTitle"  runat="server">T</h1>
        		
				<p  id="jobDes"  runat="server"></p>
				
				
				<form action="#" method="post" class="label-top" runat="server">
				    
					<div>
					    <asp:Button ID="apply" runat="server" Text="Apply" onclick="apply_Click"></asp:Button>
				    </div>
                    <a href="https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=759wbpflbgm7wc&state=123abcEFG&redirect_uri=http://114.215.168.69/posttest.aspx"> apply with linkedin</a>

                     <script type="IN/Apply" data-companyname="HRBOSS" data-jobtitle="<%=jobNameShow%>" data-jobId="<%= Request["jobid"]%>" data-url="http://114.215.168.69/posttest.aspx"></script>
                     <div class="bdsharebuttonbox"><a href="#" class="bds_more" data-cmd="more"></a><a title="分享到微信" href="#" class="bds_weixin" data-cmd="weixin"></a><a title="分享到新浪微博" href="#" class="bds_tsina" data-cmd="tsina"></a><a title="分享到人人网" href="#" class="bds_renren" data-cmd="renren"></a><a title="分享到腾讯微博" href="#" class="bds_tqq" data-cmd="tqq"></a><a title="分享到linkedin" href="#" class="bds_linkedin" data-cmd="linkedin"></a><a title="分享到Facebook" href="#" class="bds_fbook" data-cmd="fbook"></a><a title="分享到Twitter" href="#" class="bds_twi" data-cmd="twi"></a></div>
<script>    window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "", "bdMini": "2", "bdMiniList": false, "bdPic": "", "bdStyle": "0", "bdSize": "16" }, "share": {} }; with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];</script>
				</form>
		</article>
    </div>
    <footer>
    	<p></p>
    </footer>
  </div>
 
<script src="js/jquery.min.js"></script>
<script type="text/javascript">
    window.addEventListener("load", function () {
        // Set a timeout...
        setTimeout(function () {
            // Hide the address bar!
            window.scrollTo(0, 1);
        }, 0);
    });
    $('.search-box,.menu').hide();
    $('.options li:first-child').click(function () {
        $(this).toggleClass('active');
        $('.search-box').toggle();
        $('.menu').hide();
        $('.options li:last-child').removeClass('active');
    });
    $('.options li:last-child').click(function () {
        $(this).toggleClass('active');
        $('.menu').toggle();
        $('.search-box').hide();
        $('.options li:first-child').removeClass('active');
    });
    $('.content').click(function () {
        $('.search-box,.menu').hide();
        $('.options li:last-child, .options li:first-child').removeClass('active');
    });


    

</script>

</body>
</html>

