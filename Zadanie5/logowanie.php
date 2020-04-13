<?php session_start(); ?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>PHP</title>
</head>
<body>
    <?php
        require("functions.php");
        
        if (isSet($_POST["zaloguj"])) 
        {
            $login = test_input($_POST["login"]);
            $password = test_input($_POST["haslo"]);
            $_SESSION["zalogowany"] = 0;

            if ($login == $osoba1->login && $password == $osoba1->haslo) {
                $_SESSION["zalogowany"] = 1;
                $_SESSION["zalogowanyImie"] = $osoba1->imieNazwisko;
            }
            if ($login == $osoba2->login && $password == $osoba2->haslo) {
                $_SESSION["zalogowanyImie"] = $osoba2->imieNazwisko;
                $_SESSION["zalogowany"] = 1;
            }
            //echo "Login: " . $login . "<br>" . "Hasło: " . $password . "<br>";
            if ($_SESSION["zalogowany"] == 1) {
                header("Location: user.php");
            } else {
                header("Location: index.php");
            }
        }
    ?>
    <a href="/zad5/index.php">Strona główna</a>
</body>
</html>