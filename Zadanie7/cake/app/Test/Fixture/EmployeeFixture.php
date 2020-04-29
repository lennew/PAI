<?php
/**
 * Employee Fixture
 */
class EmployeeFixture extends CakeTestFixture {

/**
 * Fields
 *
 * @var array
 */
	public $fields = array(
		'id' => array('type' => 'integer', 'null' => false, 'default' => null, 'unsigned' => false, 'key' => 'primary'),
		'nazwisko' => array('type' => 'string', 'null' => true, 'default' => null, 'length' => 15, 'collate' => 'utf8_polish_ci', 'charset' => 'utf8'),
		'imie' => array('type' => 'string', 'null' => true, 'default' => null, 'length' => 15, 'collate' => 'utf8_polish_ci', 'charset' => 'utf8'),
		'etat' => array('type' => 'string', 'null' => true, 'default' => null, 'length' => 10, 'collate' => 'utf8_polish_ci', 'charset' => 'utf8'),
		'id_szefa' => array('type' => 'decimal', 'null' => true, 'default' => null, 'length' => '4,0', 'unsigned' => false),
		'zatrudniony' => array('type' => 'date', 'null' => true, 'default' => null),
		'placa_pod' => array('type' => 'decimal', 'null' => true, 'default' => null, 'length' => '6,2', 'unsigned' => false),
		'placa_dod' => array('type' => 'decimal', 'null' => true, 'default' => null, 'length' => '6,2', 'unsigned' => false),
		'id_zesp' => array('type' => 'decimal', 'null' => true, 'default' => null, 'length' => '2,0', 'unsigned' => false),
		'indexes' => array(
			'PRIMARY' => array('column' => 'id', 'unique' => 1)
		),
		'tableParameters' => array('charset' => 'utf8', 'collate' => 'utf8_polish_ci', 'engine' => 'MyISAM')
	);

/**
 * Records
 *
 * @var array
 */
	public $records = array(
		array(
			'id' => 1,
			'nazwisko' => 'Lorem ipsum d',
			'imie' => 'Lorem ipsum d',
			'etat' => 'Lorem ip',
			'id_szefa' => '',
			'zatrudniony' => '2020-04-29',
			'placa_pod' => '',
			'placa_dod' => '',
			'id_zesp' => ''
		),
	);

}
