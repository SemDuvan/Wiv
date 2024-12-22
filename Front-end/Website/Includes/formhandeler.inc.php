<?php

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = $_POST["mail_adres"];
    $pwd = $_POST["pass"];

    try {
        require_once "dbh.inc.php";

        $query = "INSERT INTO LoginUsers (mail_adres, pass) VALUES (?, ?);";
        $stmt = $pdo->prepare($query);
        $stmt->execute([$username, $pwd]);

        $pdo = null;
        $stmt = null;
        header("Location: ../login.html");
        die();
    } catch (PDOExceptiom $e) {
        die("Query failed: " . $e->getMessage());
    }
}
else {
    header("Location: ../login.html");
}