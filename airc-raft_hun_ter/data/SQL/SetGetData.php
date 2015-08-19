<?php
//khi goi thi goi nhu ben duoi chu y da ta base co it nhat 5 phan tu
//http://gamethuanviet.com/BanCaAnXuSMS1/SetGetData.php?type=select&username=Hello
//http://gamethuanviet.com/BanCaAnXuSMS1/SetGetData.php?type=update&username=toanstt&Score=12&Level=31&Played=14&country=14a

/*
  `UserName` varchar(20) NOT NULL,
  `UDID` varchar(50) NOT NULL,
  `Score` int(11) NOT NULL,
  `AddCoin` int(11) NOT NULL,
  `Level` int(11) NOT NULL,
  `CountSMS5000` int(11) NOT NULL,
  `CountSMS10000` int(11) NOT NULL,
  `CountSMS15000` int(11) NOT NULL,
  `Phone` int(11) NOT NULL,
  `CountCard10000` int(11) NOT NULL,
  `CountCard20000` int(11) NOT NULL,
  `CountCard50000` int(11) NOT NULL,
  `CountCard100000` int(11) NOT NULL,
  `CountCard200000` int(11) NOT NULL,
  `CountCard500000` int(11) NOT NULL,
  `Note` varchar(100) NOT NULL,
*/
$con = mysql_connect("localhost","gamethua_game","30xxxxx");
if (!$con)
  {
	
	die('Could not connect: ' . mysql_error());
  }


$typeCommand =  $_GET['type'];

// some code
mysql_select_db("gamethua_thuanviet", $con);

if($typeCommand == "update")
{	
	$UpdateCommand = "INSERT INTO `BanCaAnXuSMS1` (`UserName`, `Score`,`Level`, `Played`, `Country`)	" 
	."VALUES ('". $_GET["username"] 
	."'," . $_GET["Score"] 
	. "," .$_GET["Level"]  
	. "," .$_GET["Played"]  
	. ",'" . $_GET["country"] . "'"
	. ") ON DUPLICATE KEY UPDATE "
	. " `Score` = ".$_GET["Score"] .","
	//. " `Score` = IF( `Score`>" . $_GET["Score"] . ", `Score`," . $_GET["Score"]. " ),"
	. " `Played` = IF( `Played` >" . $_GET["Played"] . ", `Played`," . $_GET["Played"]. " )";	
	echo $UpdateCommand;
	$result = mysql_query($UpdateCommand);
	
}else if($typeCommand == "select")
{
	$selectCommand = "SELECT * FROM `BanCaAnXuSMS1` ORDER BY `Score` DESC LIMIT 0 , 10";
	//$Insert_UpdateCommand = "INSERT INTO `BanCaAnXuSMS1` (`UserName`, `Score`, `Country`, `Unknow`) VALUES (`CaoGia`, 1,'VN','UnKnow') ON DUPLICATE KEY UPDATE `Score` = 1234"
	$result = mysql_query($selectCommand);
	echo mysql_num_rows ($result)."|";
	while($row = mysql_fetch_array($result))
	{
		echo $row['UserName'] . "|" . $row['Score']. "|" . $row['Level'];
		echo "|";
	}
	
	//user rank
	$selectCommand = "SELECT COUNT( * ) AS rank FROM  `BanCaAnXuSMS1` WHERE  `Score` >= ( SELECT  `Score` FROM  `BanCaAnXuSMS1` WHERE  `UserName` ='".$_GET["username"]. "')";
	
	//echo $selectCommand;
	$result = mysql_query($selectCommand);	
	while($row = mysql_fetch_array($result))
	{
		echo $row['rank']. "|";
	}
	//user info
	$username = $_GET["username"];
	$selectCommand = "SELECT *   FROM  `BanCaAnXuSMS1` WHERE  `UserName` =  '".$username."'";
	//echo $selectCommand ;
	$result = mysql_query($selectCommand);
	while($row = mysql_fetch_array($result))
	{
		echo $row['UserName'] . "|" . $row['Score']. "|" . $row['Level'];
		echo "|";
	}
}

//end some code
mysql_close($con);
?> 


