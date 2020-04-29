<h1>Edytuj książkę</h1>

<?php $options = array('dramat' => 'dramat', 'komedia' => 'komedia');
    echo $this->Form->create('Book', array('url' => 'edit'));
    echo $this->Form->input('id', array('type' => 'hidden'));
    echo $this->Form->input('title');
    echo $this->Form->input('author');
    echo $this->Form->input('genre', array('options' => $options, 'default' => 'dramat'));
    echo $this->Form->end('Zapisz');
?>