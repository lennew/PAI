<?php
App::uses('AppModel', 'Model');
/**
 * Employee Model
 *
 */
class Employee extends AppModel {
    var $validate = array(
        'nazwisko'=> array('rule' => 'notBlank'), 
        'etat' => array('rule' => 'notBlank'),
        'placa_pod' => array(
            'rule' => array('range', 0, 2000),
            'message' => 'Please enter a number between 0 and 2000')
        );
}
