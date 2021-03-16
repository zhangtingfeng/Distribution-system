<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
<%@ page import="java.io.*" %>
<%@ page import="java.awt.image.BufferedImage" %>
<%@ page import="javax.imageio.stream.ImageOutputStream" %>
<%@ page import="javax.imageio.*" %>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>My JSP 'index.jsp' starting page</title>
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->
  </head>
  
  <body>
    <%
    String width = request.getParameter("width");
    String height = request.getParameter("height");
    int w = Integer.parseInt(width);
    int h = Integer.parseInt(height);
try {
    BufferedImage bf = new BufferedImage(w, h, BufferedImage.TYPE_INT_RGB);
    for (int i = 0; i < bf.getHeight(); i++) {
     String data = request.getParameter("px" + i);
     String[] ds = data.split(",");
     for (int j = 0; j < bf.getWidth(); j++) {
      int d = Integer.parseInt(ds[j], 16);
      bf.setRGB(j, i, d);
     }
    }

    ImageWriter writer = null;
    ImageTypeSpecifier type = ImageTypeSpecifier.createFromRenderedImage(bf);
    Iterator iter = ImageIO.getImageWriters(type, "jpg");
    if (iter.hasNext()) {
     writer = (ImageWriter) iter.next();
    }
    IIOImage iioImage = new IIOImage(bf, null, null);
    ImageWriteParam param = writer.getDefaultWriteParam();
    param.setCompressionMode(ImageWriteParam.MODE_EXPLICIT);
    param.setCompressionQuality(0.2f);
    // save to file
    FileOutputStream fossss=new FileOutputStream("c://temp.jpg");
    ImageOutputStream outputStream2 = ImageIO.createImageOutputStream(fossss);
    writer.setOutput(outputStream2);
    writer.write(null, iioImage, param);
    outputStream2.close();
    fossss.close();
    writer.notifyAll();
   } catch (Exception e) {
    e.printStackTrace();
   }
   System.out.println("===================");
   request.getSession().setAttribute("p","c://temp.jpg");
   response.sendRedirect("addStudent.jsp");
     %>
  </body>
</html>
