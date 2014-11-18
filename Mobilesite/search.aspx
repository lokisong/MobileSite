<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="search" %>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="utf-8" />
  <title>Single - Galaxy Mobi</title>
  <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0" /> 
  <link rel="stylesheet" media="all" href="style.css" type="text/css">
</head>

<body>
<div class="wrap">
    <header>
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
        	</ul>
	    </nav>
    </header>
    
        
    <div class="content">
    	<article>
				
				<h3 class="underline">Search Job</h3>
				<form action="#" method="post" class="label-top">
				    <div>
				         <label for="name">Name</label>
				         <input type="text" name="name" id="name" value="" tabindex="1" />
				    </div>
				    <div>
				         <label for="location">Location</label>
                         <select id="location" tabindex="1">
                            <option>北京</option>
                            <option>上海</option>
                            <option>广州</option>
                            </select>
				    </div>
				    <div>
				         <label for="function">Function</label>
				         <input type="text" name="function" id="function" value="" tabindex="1" />
				    </div>
				    
					<div>
					    <input type="Submit" value="Search" />
				    </div>
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
