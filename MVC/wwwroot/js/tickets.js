$(function () {
    const tickets = window.ticketsData || [];

    // Update Ticket
    $('[id^="edit-"]').on('click', function (e) {
        e.preventDefault();

        const $btn = $(this);
        const id = parseInt($btn.attr('id').split('-')[1]);
        const $nameEl = $(`#name-${id}`);
        const $descEl = $(`#description-${id}`);
        const $card = $(`#ticket-${id}`);

        let ticket = tickets.Tickets.find(t => t.id === id || t.Id === id);
        const editing = $card.data('editing') === true;

        if (!editing) {
            const nameVal = $.trim($nameEl.text());
            const descVal = $.trim($descEl.text());

            $nameEl.html(`<input id="input-name-${id}" class="form-control form-control-sm" value="${nameVal}" />`);
            $descEl.html(`<textarea id="input-desc-${id}" class="form-control form-control-sm" rows="2">${descVal}</textarea>`);

            $btn.text('Save');
            $card.data('editing', true);
        } else {
            const newName = $(`#input-name-${id}`).val();
            const newDesc = $(`#input-desc-${id}`).val();

            $nameEl.text(newName);
            $descEl.text(newDesc);
            $btn.text('Edit');
            $card.data('editing', false);

            if (!ticket) {
                console.error("Ticket no encontrado para actualizar");
                return;
            }

            $.ajax({
                url: `https://localhost:7169/api/Task`,
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify({
                    id: ticket.id ?? ticket.Id,
                    name: newName,
                    description: newDesc,
                    createdBy: ticket.createdBy ?? ticket.CreatedBy,
                    createdDate: ticket.createdDate ?? ticket.CreatedDate,
                    status: ticket.status ?? ticket.Status,
                    Approval: ticket.Approval ?? ticket.Approval
                }),
                success: function () {
                    alert("Ticket updated!");
                },
                error: function () {
                    alert("Error updating ticket");
                }
            });
        }
    });

    // --- delete ticket ---
    $('[id^="delete-"]').on('click', function (e) {
        e.preventDefault();

        const id = parseInt($(this).data('id'));
        if (!confirm(`Are you sure you want to delete ticket #${id}?`)) return;

        $.ajax({
            url: `https://localhost:7169/api/Task/${id}`,
            type: "DELETE",
            success: function () {
                alert(`Ticket #${id} deleted!`);
                $(`#ticket-${id}`).remove();
            },
            error: function () {
                alert("Error deleting ticket");
            }
        });
    });

    // --- add ticket ---
    const $addBtn = $('#addTicketBtn');
    const $form = $('#addTicketForm');
    const $saveBtn = $('#saveTicketBtn');
    const $cancelBtn = $('#cancelTicketBtn');

    $addBtn.on('click', function () {
        $form.removeClass('d-none');
        $addBtn.addClass('d-none');
    });

    $cancelBtn.on('click', function () {
        $form.addClass('d-none');
        $addBtn.removeClass('d-none');
    });

});
