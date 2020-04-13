<?php
    function test_input($data) {
        $data = trim($data);
        $data = stripslashes($data);
        $data = htmlspecialchars($data);
        return $data;
    }

    function check_upload() {
        $currentDir = getcwd();
        $uploadDirectory = "\\zdjeciaUzytkownikow\\";
        $fileName = "myphoto.jpg";
        $fileSize = $_FILES['myfile']['size'];
        $fileTmpName = $_FILES['myfile']['tmp_name'];
        $fileType=$_FILES['myfile']['type'];
        if ($fileName != "" and
            ($fileType == 'image/png' or $fileType == 'image/jpeg'
            or $fileType == 'image/JPEG' or $fileType == 'image/PNG')
        ) {
           $uploadPath = $currentDir . $uploadDirectory . $fileName;
           if(move_uploaded_file($fileTmpName, $uploadPath))
            echo "Zdjecie zostalo zaladowane<br>"; 
        }
    }

    class Osoba {
        public $login;
        public $haslo;
        public $imieNazwisko;
    }
        
        $osoba1 = new Osoba;
        $osoba1->login = "adam";
        $osoba1->haslo = "adam2020";
        $osoba1->imieNazwisko = "Adam Kowalski";
        
        $osoba2 = new Osoba;
        $osoba2->login = "agata";
        $osoba2->haslo = "2020agata";
        $osoba2->imieNazwisko = "Agata Nowak";
?>