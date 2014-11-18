<%@ Page Language="C#" AutoEventWireup="true" CodeFile="apply.aspx.cs" Inherits="apply" %>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="utf-8" />  
  <title>Contact - Galaxy Mobi</title>
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
	            <li><a href="search.aspx">Search</a></li>
        	</ul>
	    </nav>
    </header>
    
        
    <div class="content">
    	<article>
    	<h2 class="underline">Application Form</h2>
			<form action="#" method="post" class="label-top" runat=server>
			    <div>
			         <label for="firstname">First Name<span class="red">*</span></label>
                     <asp:TextBox ID="name" runat="server"></asp:TextBox>
			    </div>
                <div>
			         <label for="lastname">Last Name<span class="red">*</span></label>
                     <asp:TextBox ID="lastName" runat="server"></asp:TextBox>
			    </div>
			    <div>
			         <label for="email">Email <span class="red">*</span></label>
                     <asp:TextBox ID="email" runat="server"></asp:TextBox>
			    </div>
                 <div>
			         <label for="phone">Phone <span class="red">*</span></label>
                     <asp:TextBox ID="phone" runat="server"></asp:TextBox>
			    </div>
                <div>
			         <label for="years">Years of Working Experience <span class="red">*</span></label>
                     <asp:TextBox ID="years" runat="server"></asp:TextBox>
			    </div>
                <div>
			         <label for="company">Current Company <span class="red">*</span></label>
                     <asp:TextBox ID="company" runat="server"></asp:TextBox>
			    </div>
                <div>
			         <label for="position">Current Position <span class="red">*</span></label>
                     <asp:TextBox ID="position" runat="server"></asp:TextBox>
			    </div>
			    <div>
			         <label for="education">Education <span class="red">*</span></label>
			         <select id="education" tabindex="1" runat="server">
                            <option value="0">Please select</option>
                            <option value="1">博士</option>
                            <option value="2">硕士</option>
                            <option value="4">本科</option>
                            <option value="10">大专</option>
                            <option value="5">中专</option>
                            <option value="6">中技</option>
                            <option value="10">高中</option>
                            <option value="10">初中</option>
                            <option value="0">其他</option>
                            </select>
			    </div>
			    <div>
					<label for="Background">Self Assessment<span class="red">*</span></label>
					<textarea cols="40" rows="8" name="Background" id="Background" runat="server"></textarea>
				</div>
				<div>
				    <asp:Button ID="submit" runat="server" Text="Submit" onclick="submit_Click"></asp:Button>
                                                         
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
