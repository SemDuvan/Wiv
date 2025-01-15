<?php

$dsn = "mssql:host=4.210.227.204;dbname=testdb";
$dbusername = "sa";
$dbpassword = "LengthyP@ssword1";

try {
    $pdo = new PDO($dsn, $dbusername, $dbpassword);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION)
} catch (PDOExceptiom $e) {
    echo "Connection failed: " . $e->getMessage();
}