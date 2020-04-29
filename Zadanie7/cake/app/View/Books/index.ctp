<h1>Books</h1>
<p>
    <?php echo $this->Html->link('Dodaj', array('action' => 'add')); ?>
</p>
<table>
    <tr>
        <th>Id</th>
        <th>Title</th>
        <th>Author</th>
        <th>Genre</th>
        <th></th>
    </tr>

    <?php foreach ($books as $book) : ?>
        <tr>
            <td>
                <?php echo $book['Book']['id']; ?>
            </td>
            <td>
                <?php echo $this->Html->link(
                    $book['Book']['title'],
                    array('action' => 'view', $book['Book']['id'])
                ); ?>
            </td>
            <td>
                <?php echo $book['Book']['author']; ?>
            </td>
            <td>
                <?php echo $book['Book']['genre']; ?>
            </td>
            <td>
                <?php echo $this->Html->link(
                    'Usuń',
                    array('action' => 'delete', $book['Book']['id']),
                    null,
                    'Czy jesteś pewna(y)?'
                ) ?>
                <?php echo $this->Html->link(
                    'Edytuj',
                    array('action' => 'edit', $book['Book']['id'])
                ); ?>
            </td>
        </tr>
    <?php endforeach; ?>
</table>