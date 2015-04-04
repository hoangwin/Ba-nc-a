
<?php

	//http://gamethuanviet.com/commond_sms/SMS.php?moid=VIETEL1367522&service_num=7595&phone=84989538859&syntax=NMH&message=NMh&user=NiscoSTM
	//http://gamethuanviet.com/commond_sms/SMS.php?moid=VIETEL1367522&service_num=7595&phone=84989538859&syntax=NMH&message=NMh BCTC toilaai&user=NiscoSTM
	//tin nhan nguoi goi se la " STM BDCL username
	//tin nhan nguoi goi se la " NMH BAUCUA username
/*
//////////////////////////ban_ca_an_xu_SMS////////////////////////////
  `UserName` varchar(50) NOT NULL,
  `UDID` varchar(100) NOT NULL,
  `SMS` int(11) NOT NULL,
  `CARD` int(11) NOT NULL,
  `DATE` int(11) NOT NULL,
  `NOTE` int(11) NOT NULL
///////////////////////////////ban_ca_an_xu////////////////////////
    `UserName` varchar(50) NOT NULL,
  `UDID` varchar(100) NOT NULL,
  `Score` int(11) NOT NULL,
  `Level` int(11) NOT NULL,
  `Played` int(11) NOT NULL,
  `AddCoin` int(11) NOT NULL,
  `Note` int(11) NOT NULL  
*/
  
function ban_ca_an_xu_SMS($U,$T,$PHONE,$SERVICE_NUM)
{
	$con = mysql_connect("localhost","gamethua_game","30xxxxx");
	if (!$con)
	{
		die('Could not connect: ' . mysql_error());
	}
	// some code
	mysql_select_db("gamethua_thuanviet", $con);
	if($SERVICE_NUM =="7595")
	{
		$T = 5000;
		$Coin = 1500;
	}
	else if($SERVICE_NUM =="7695")
	{
		$T = 10000;
		$Coin = 3150;
	}
	else //7795
	{
		$T = 15000;
		$Coin = 4950;
	}	
	
	/*
  `UserName` varchar(50) NOT NULL,
  `UDID` varchar(100) NOT NULL,
  `SMS` int(11) NOT NULL,
  `CARD` int(11) NOT NULL,
  `DATE` int(11) NOT NULL,
  `NOTE` int(11) NOT NULL
	*/
	$result = mysql_query("insert into ban_ca_an_xu_SMS (UDID,Phone,SMS) values ('$U', '$PHONE','$T')");	
	echo $result;
	if (!empty($result))
	{		
			echo "200| [Ban Ca An Xu] Ban vua nap $Coin Coin vao tai khoan '$U' ";
			$result  = mysql_query("select * from ban_ca_an_xu where UDID='$U'");		
			if($row = mysql_fetch_array($result))
			{			
				$Coin += $row['AddCoin'];		
				mysql_query("update ban_ca_an_xu set AddCoin= '$Coin' where UDID='$U'");
			}
			else
			{		
				mysql_query("insert into ban_ca_an_xu (UDID, AddCoin) values ('$U', '$Coin')");
			}
	}
	else
	{	
		echo "204| [Ban Ca An Xu] Loi khong ro nguyen nhan";
	}
	mysql_close($con);
}
?>

