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
        echo "<h1>Nasz system</h1>";

        if(isSet($_POST["wyloguj"])) {
            $_SESSION["zalogowany"] = 0;
        }        
    ?>
    <form action="logowanie.php" method="post">
        <fieldset>
            <legend>Logowanie</legend>
            Login: <input type="text" name="login"><br>
            Hasło: <input type="text" name="haslo"><br>
            <input type="submit" name="zaloguj" value="Zaloguj">
        </fieldset>
    </form>

    <form action="cookie.php" method="get">
        <fieldset>
            <legend>Cookies</legend>
            <input type="number" name="czas" required value=0><br>
            <input type="submit" name="utworzCookie" value="Utwórz cookie"><br>
            <?php
                if (isSet($_COOKIE['mycookie']))
                    echo "Ciasteczko ustawione: " . $_COOKIE['mycookie'] . "<br>";
                else
                    echo "Nie ma ciasteczka!<br>";
            ?>
        </fieldset>
    </form>
    <a href="/zad5/user.php">Strona użytkownika</a>
</body>
</html>