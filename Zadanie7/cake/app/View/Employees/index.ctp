<div class="employees index">
	<h2><?php echo __('Employees'); ?></h2>
	<table cellpadding="0" cellspacing="0">
	<thead>
	<tr>
			<th><?php echo $this->Paginator->sort('id'); ?></th>
			<th><?php echo $this->Paginator->sort('nazwisko'); ?></th>
			<th><?php echo $this->Paginator->sort('imie'); ?></th>
			<th><?php echo $this->Paginator->sort('etat'); ?></th>
			<th><?php echo $this->Paginator->sort('id_szefa'); ?></th>
			<th><?php echo $this->Paginator->sort('zatrudniony'); ?></th>
			<th><?php echo $this->Paginator->sort('placa_pod'); ?></th>
			<th><?php echo $this->Paginator->sort('placa_dod'); ?></th>
			<th><?php echo $this->Paginator->sort('id_zesp'); ?></th>
			<th class="actions"><?php echo __('Actions'); ?></th>
	</tr>
	</thead>
	<tbody>
	<?php foreach ($employees as $employee): ?>
	<tr>
		<td><?php echo h($employee['Employee']['id']); ?>&nbsp;</td>
		<td><?php echo h($employee['Employee']['nazwisko']); ?>&nbsp;</td>
		<td><?php echo h($employee['Employee']['imie']); ?>&nbsp;</td>
		<td><?php echo h($employee['Employee']['etat']); ?>&nbsp;</td>
		<td><?php echo h($employee['Employee']['id_szefa']); ?>&nbsp;</td>
		<td><?php echo h($employee['Employee']['zatrudniony']); ?>&nbsp;</td>
		<td><?php echo h($employee['Employee']['placa_pod']); ?>&nbsp;</td>
		<td><?php echo h($employee['Employee']['placa_dod']); ?>&nbsp;</td>
		<td><?php echo h($employee['Employee']['id_zesp']); ?>&nbsp;</td>
		<td class="actions">
			<?php echo $this->Html->link(__('View'), array('action' => 'view', $employee['Employee']['id'])); ?>
			<?php echo $this->Html->link(__('Edit'), array('action' => 'edit', $employee['Employee']['id'])); ?>
			<?php echo $this->Form->postLink(__('Delete'), array('action' => 'delete', $employee['Employee']['id']), array('confirm' => __('Are you sure you want to delete # %s?', $employee['Employee']['id']))); ?>
		</td>
	</tr>
<?php endforeach; ?>
	</tbody>
	</table>
	<p>
	<?php
	echo $this->Paginator->counter(array(
		'format' => __('Page {:page} of {:pages}, showing {:current} records out of {:count} total, starting on record {:start}, ending on {:end}')
	));
	?>	</p>
	<div class="paging">
	<?php
		echo $this->Paginator->prev('< ' . __('previous'), array(), null, array('class' => 'prev disabled'));
		echo $this->Paginator->numbers(array('separator' => ''));
		echo $this->Paginator->next(__('next') . ' >', array(), null, array('class' => 'next disabled'));
	?>
	</div>
</div>
<div class="actions">
	<h3><?php echo __('Actions'); ?></h3>
	<ul>
		<li><?php echo $this->Html->link(__('New Employee'), array('action' => 'add')); ?></li>
	</ul>
</div>
