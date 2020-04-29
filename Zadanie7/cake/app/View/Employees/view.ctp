<div class="employees view">
<h2><?php echo __('Employee'); ?></h2>
	<dl>
		<dt><?php echo __('Id'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['id']); ?>
			&nbsp;
		</dd>
		<dt><?php echo __('Nazwisko'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['nazwisko']); ?>
			&nbsp;
		</dd>
		<dt><?php echo __('Imie'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['imie']); ?>
			&nbsp;
		</dd>
		<dt><?php echo __('Etat'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['etat']); ?>
			&nbsp;
		</dd>
		<dt><?php echo __('Id Szefa'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['id_szefa']); ?>
			&nbsp;
		</dd>
		<dt><?php echo __('Zatrudniony'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['zatrudniony']); ?>
			&nbsp;
		</dd>
		<dt><?php echo __('Placa Pod'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['placa_pod']); ?>
			&nbsp;
		</dd>
		<dt><?php echo __('Placa Dod'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['placa_dod']); ?>
			&nbsp;
		</dd>
		<dt><?php echo __('Id Zesp'); ?></dt>
		<dd>
			<?php echo h($employee['Employee']['id_zesp']); ?>
			&nbsp;
		</dd>
	</dl>
</div>
<div class="actions">
	<h3><?php echo __('Actions'); ?></h3>
	<ul>
		<li><?php echo $this->Html->link(__('Edit Employee'), array('action' => 'edit', $employee['Employee']['id'])); ?> </li>
		<li><?php echo $this->Form->postLink(__('Delete Employee'), array('action' => 'delete', $employee['Employee']['id']), array('confirm' => __('Are you sure you want to delete # %s?', $employee['Employee']['id']))); ?> </li>
		<li><?php echo $this->Html->link(__('List Employees'), array('action' => 'index')); ?> </li>
		<li><?php echo $this->Html->link(__('New Employee'), array('action' => 'add')); ?> </li>
	</ul>
</div>
