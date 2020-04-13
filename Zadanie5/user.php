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
        if ($_SESSION["zalogowany"] == 0) {
            header("Location: index.php");
        }

        if(isSet($_POST["upload"])) {
            check_upload();
        }
    ?>
    <form action="index.php" method="post">
        <fieldset>
            <legend><?php echo "Użytkownik: " . $_SESSION["zalogowanyImie"];?></legend>
            <input type="submit" name="wyloguj" value="Wyloguj"><br>
            <a href="/zad5/index.php">Strona główna</a>
        </fieldset>
    </form>

    <form action="user.php" method="post" enctype="multipart/form-data">
        <fieldset>
            <legend>Wrzucanie pliku</legend>
            <input name="myfile" type="file"><br>
            <input type="submit" name="upload" value="Wrzuć plik"><br>
            <img src="zdjeciaUzytkownikow\myphoto.jpg" alt="Wrzuć zdjęcie, aby je wyświetlić">
        </fieldset>
    </form>
    
</body>
</html>