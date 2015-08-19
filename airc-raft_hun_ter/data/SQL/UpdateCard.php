<?php
//khi goi thi goi nhu ben duoi chu y da ta base co it nhat 5 phan tu
//http://gamethuanviet.com/ban_ca_an_xu/UpdateCard.php?username=Hello&card=500000
	$con = mysql_connect("localhost","gamethua_game","30xxxxx");
	if (!$con)
	{
		echo "Loi Connect";
		die('Could not connect: ' . mysql_error());
	}

	// some code
	mysql_select_db("gamethua_thuanviet", $con);

	$username = $_GET["username"];	
	$card = $_GET["card"];	
	
	mysql_query("insert into ban_ca_an_xu_SMS (UDID, CARD) values ('$username', '$card')");
	mysql_close($con);
?> 
