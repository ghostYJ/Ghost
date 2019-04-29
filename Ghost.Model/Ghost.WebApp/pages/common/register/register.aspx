<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <!--ie模拟渲染-->
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>注册</title>
    <link href="../bootstrap-3.3.7/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../../resources/js/jquery-ui-1.10.4/js/jquery-1.10.2.js"></script>
    <style>
        .center {
            margin: 0 auto;
            margin-top: 200px;
            border: 0px solid #aaa;
            width: 300px;
            height: 300px;
        }
    </style>
    <script type="text/javascript">
        //切换验证码
        function ToggleCode(obj, codeurl) {
            $(obj).children("img").eq(0).attr("src", codeurl + "?time=" + Math.random());
            return false;
        }
    </script>
</head>
<body style="background-image: url('../../../resources/images/timg.jpg')">
    <div class="center">
        <form action="" runat="server">
            <div class="form-group">
                <span style="font-weight: bold">手机号</span>
                <asp:TextBox runat="server" class="form-control" ID="tbMobile" placeholder="输入手机号" />
            </div>
            <div class="form-group">
                <span style="font-weight: bold">密码</span>
                <asp:TextBox runat="server" class="form-control" ID="tbPassword" placeholder="输入密码" />
            </div>
            <div class="form-group">
                <span style="font-weight: bold">验证码</span>
                <asp:TextBox ID="tbCode" runat="server" placeholder="输入验证码" Width="300px" class="form-control"></asp:TextBox>
                <a class="send" title="点击切换验证码" href="javascript:;" onclick="ToggleCode(this, 'VerifyCode.ashx');return false;">
                    <img src="VerifyCode.ashx" width="80" height="22" />
                </a>
            </div>
            <asp:Button runat="server" ID="btnSave" class="btn btn-default" Text="提交" />
        </form>
    </div>
</body>
</html>
