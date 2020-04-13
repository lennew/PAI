<?php session_start(); ?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>PHP</title>
</head>
<body>
    <?php
        require_once("functions.php");
        if(isSet($_GET["utworzCookie"])) {
            $cookieTime = $_GET["czas"];
            setcookie("mycookie", "mycookievalue", time() + $cookieTime, "/");
            echo "<p>Utworzono ciasteczko!</p>";
        }
    ?>
    <a href="/zad5/index.php">Wstecz</a>
</body>
</html>