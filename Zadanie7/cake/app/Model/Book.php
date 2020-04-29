<?php
    class Book extends AppModel {
        var $name = 'Book';
        var $validate = array('title'=> array('rule' => 'notBlank'));
    }
?>
