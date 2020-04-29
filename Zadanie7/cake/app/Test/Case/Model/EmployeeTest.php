<?php
App::uses('Employee', 'Model');

/**
 * Employee Test Case
 */
class EmployeeTest extends CakeTestCase {

/**
 * Fixtures
 *
 * @var array
 */
	public $fixtures = array(
		'app.employee'
	);

/**
 * setUp method
 *
 * @return void
 */
	public function setUp() {
		parent::setUp();
		$this->Employee = ClassRegistry::init('Employee');
	}

/**
 * tearDown method
 *
 * @return void
 */
	public function tearDown() {
		unset($this->Employee);

		parent::tearDown();
	}

}
