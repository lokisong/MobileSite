<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" Debug="true"%>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="utf-8" />
  <title>HR Boss - Job List</title>
  <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0" /> 
  <link rel="stylesheet" media="all" href="style.css" type="text/css">
  <script src="js/jquery.min.js"></script>
</head>

<body>
<div class="wrap">
    <header>
      <div class="logo"><a href="index.aspx"><img src="images/logo.png" alt=""/></a></div>
      <div class="options">
      	<ul>
      		<li>Search</li>
      		<li>Menu</li>
      	</ul>
      </div>
      <div class="clear"></div>
	    <div class="search-box">
	    	<form action="#" runat="server">
	    		<input type="text" placeholder="Search job" id="jobSearchText" runat="server"/>
                <asp:Button ID="jobSearchSubmit" runat="server" Text="Go" onclick="jobSearchSubmit_Click"></asp:Button>
	    	</form>
	    	<div class="clear"></div>
	    </div>
	    <nav class="vertical menu">
	    	<ul>
	            <li><a href="index.aspx">Home</a></li>
	            <li><a href="search.aspx">Search jobs</a></li>
        	</ul>
	    </nav>
    </header>

    <div class="content" id="hbJobContent" runat="server">
<%--	<div class="paginate">
		<ul>
			<li><span class="current">1</span></li>
			<li><a href="#2" class="inactive">2</a></li>
			<li><a href="#" class="inactive">3</a></li>
			<li><a href="#">&#62;</a></li>
			<li><a href="#">&raquo;</a></li>
		</ul>
	</div>--%>

    </div>
    <footer>
    	<p></p>
    </footer>
  </div>
 

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
