<?php
App::uses('AppController', 'Controller');
/**
 * Employees Controller
 *
 * @property Employee $Employee
 * @property PaginatorComponent $Paginator
 */
class EmployeesController extends AppController {

/**
 * Components
 *
 * @var array
 */
	public $components = array('Paginator');

/**
 * index method
 *
 * @return void
 */
	public function index() {
		$this->Employee->recursive = 0;
		$this->set('employees', $this->Paginator->paginate());
	}

/**
 * view method
 *
 * @throws NotFoundException
 * @param string $id
 * @return void
 */
	public function view($id = null) {
		if (!$this->Employee->exists($id)) {
			throw new NotFoundException(__('Invalid employee'));
		}
		$options = array('conditions' => array('Employee.' . $this->Employee->primaryKey => $id));
		$this->set('employee', $this->Employee->find('first', $options));
	}

/**
 * add method
 *
 * @return void
 */
	public function add() {
		if ($this->request->is('post')) {
			$this->Employee->create();
			if ($this->Employee->save($this->request->data)) {
				$this->Flash->success(__('The employee has been saved.'));
				return $this->redirect(array('action' => 'index'));
			} else {
				$this->Flash->error(__('The employee could not be saved. Please, try again.'));
			}
		}
	}

/**
 * edit method
 *
 * @throws NotFoundException
 * @param string $id
 * @return void
 */
	public function edit($id = null) {
		if (!$this->Employee->exists($id)) {
			throw new NotFoundException(__('Invalid employee'));
		}
		if ($this->request->is(array('post', 'put'))) {
			if ($this->Employee->save($this->request->data)) {
				$this->Flash->success(__('The employee has been saved.'));
				return $this->redirect(array('action' => 'index'));
			} else {
				$this->Flash->error(__('The employee could not be saved. Please, try again.'));
			}
		} else {
			$options = array('conditions' => array('Employee.' . $this->Employee->primaryKey => $id));
			$this->request->data = $this->Employee->find('first', $options);
		}
	}

/**
 * delete method
 *
 * @throws NotFoundException
 * @param string $id
 * @return void
 */
	public function delete($id = null) {
		if (!$this->Employee->exists($id)) {
			throw new NotFoundException(__('Invalid employee'));
		}
		$this->request->allowMethod('post', 'delete');
		if ($this->Employee->delete($id)) {
			$this->Flash->success(__('The employee has been deleted.'));
		} else {
			$this->Flash->error(__('The employee could not be deleted. Please, try again.'));
		}
		return $this->redirect(array('action' => 'index'));
	}
}
