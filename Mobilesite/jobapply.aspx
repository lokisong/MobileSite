<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jobapply.aspx.cs" Inherits="jobapply" %>

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
            <h2 id="jobTitle" runat="server"></h2>
            

                <form id="Form2" action="" method="post" class="label-top" runat="server">

                <div >
				     <label for="firstname">名字<span class="red">*</span></label>
                     <asp:TextBox ID="name" runat="server"></asp:TextBox>
					</div>
                <div>
			         <label for="lastname">姓氏<span class="red">*</span></label>
                     <asp:TextBox ID="lastName" runat="server"></asp:TextBox>
			    </div>
			    <div>
			         <label for="email">Email <span class="red">*</span></label>
                     <asp:TextBox ID="email" runat="server"></asp:TextBox>
			    </div>
                 <div>
			         <label for="phone">手机 <span class="red">*</span></label>
                     <asp:TextBox ID="phone" runat="server"></asp:TextBox>
			    </div>
                <div>
			         <label for="years">工作年限<span class="red">*</span></label>
                     <select id="years" tabindex="1" runat="server">
                            <option value="0">请选择</option>
                            <option value="1">1年</option>
                            <option value="2">2年</option>
                            <option value="4">3年</option>
                            <option value="10">4年</option>
                            <option value="5">5年</option>
                            <option value="6">6年</option>
                            <option value="7">7年</option>
                            <option value="8">8年</option>
                            <option value="9">9年</option>
                            <option value="10">10-15年</option>
                            <option value="16">16-20年</option>
                            <option value="20">20年以上</option>
                            </select>
			    </div>
                <div>
			         <label for="company">现在所在公司 <span class="red">*</span></label>
                     <asp:TextBox ID="company" runat="server"></asp:TextBox>
			    </div>
                <div>
			         <label for="position">现在的职位<span class="red">*</span></label>
                     <asp:TextBox ID="position" runat="server"></asp:TextBox>
			    </div>
			    <div>
			         <label for="education">学历<span class="red">*</span></label>
			         <select id="education" tabindex="1" runat="server">
                            <option value="0">请选择</option>
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
					<label for="Background">自我评价<span class="red">*</span></label>
					<textarea cols="40" rows="8" name="Background" id="Background" runat="server"></textarea>
				</div>
				<div class="ui-grid-a">			        
                    <div class="ui-block-a">
				    <asp:Button ID="submit" runat="server" Text="提交" onclick="submit_Click"></asp:Button>
                    </div>
                     <div class="ui-block-b">
                    <asp:Button ID="backBtn" class="ui-btn-check" 
                            runat="server" Text="返回" data-theme="a" onclick="backBtn_Click" />
                    </div>                    
                </div>
                                                         
			    </div>

            </form>     
           
            </div>
            
        </div>

         

	</body>
</html>