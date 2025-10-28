//esto es que lo estaba haciendo con etiquetas pero por tiempo lo tuve que pasar asi sino agrego todo con funciones jajajaja

document.addEventListener("DOMContentLoaded", function () {
    const tickets = window.ticketsData || [];

    document.querySelectorAll('[id^="edit-"]').forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();

            const id = parseInt(button.id.split("-")[1]);
            const nameEl = document.getElementById(`name-${id}`);
            const descEl = document.getElementById(`description-${id}`);
            const card = document.getElementById(`ticket-${id}`);

            let ticket = null;
            for (let i = 0; i < tickets.length; i++) {
                if (tickets[i].id === id || tickets[i].Id === id) {
                    ticket = tickets[i];
                    break;
                }
            }

            const editing = card.dataset.editing === "true";

            if (!editing) {
                const nameVal = nameEl.innerText.trim();
                const descVal = descEl.innerText.trim();

                nameEl.innerHTML = `<input id="input-name-${id}" class="form-control form-control-sm" value="${nameVal}" />`;
                descEl.innerHTML = `<textarea id="input-desc-${id}" class="form-control form-control-sm" rows="2">${descVal}</textarea>`;

                button.innerText = "Save";
                card.dataset.editing = "true";
            } else {
                const newName = document.getElementById(`input-name-${id}`).value;
                const newDesc = document.getElementById(`input-desc-${id}`).value;

                nameEl.innerHTML = newName;
                descEl.innerHTML = newDesc;
                button.innerText = "Edit";
                card.dataset.editing = "false";

                if (!ticket) {
                    console.error("Ticket no encontrado para actualizar");
                    return;
                }

                fetch(`https://localhost:7169/api/Tickets/${id}`, {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        id: ticket.id ?? ticket.Id,
                        name: newName,
                        description: newDesc,
                        createdBy: ticket.createdBy ?? ticket.CreatedBy,
                        createdDate: ticket.createdDate ?? ticket.CreatedDate,
                        status: ticket.status ?? ticket.Status
                    })
                })
                    .then(r => r.ok ? alert("Ticket updated!") : alert("Error updating ticket"))
                    .catch(err => console.error("Error:", err));
            }
        });
    });

     //Eliminar
    document.querySelectorAll('[id^="delete-"]').forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();

            const id = parseInt(button.dataset.id);
            const confirmDelete = confirm(`Are you sure you want to delete ticket #${id}?`);

            if (!confirmDelete) return;

            fetch(`https://localhost:7169/api/Tickets/${id}`, {
                method: "DELETE"
            })
                .then(r => {
                    if (r.ok) {
                        alert(`Ticket #${id} deleted!`);
                        const card = document.getElementById(`ticket-${id}`);
                        if (card) card.remove(); 
                    } else {
                        alert("Error deleting ticket");
                    }
                })
                .catch(err => {
                    console.error("Delete error:", err);
                    alert(" Could not connect to the API");
                });
        });
    });

    const addBtn = document.getElementById("addTicketBtn");
    const form = document.getElementById("addTicketForm");
    const saveBtn = document.getElementById("saveTicketBtn");
    const cancelBtn = document.getElementById("cancelTicketBtn");

    addBtn?.addEventListener("click", () => {
        form.classList.remove("d-none");
        addBtn.classList.add("d-none");
    });

    cancelBtn?.addEventListener("click", () => {
        form.classList.add("d-none");
        addBtn.classList.remove("d-none");
    });

    saveBtn?.addEventListener("click", async () => {
        const name = document.getElementById("newName").value.trim();
        const description = document.getElementById("newDescription").value.trim();
        let createdBy = document.getElementById("createdBy").value.trim();

        if (!name || !description) {
            alert("Please fill out all fields");
            return;
        }

        if (createdBy.length === 0) {
            createdBy = "Computer";
        }

        try {
            const response = await fetch("https://localhost:7169/api/Tickets", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({id:0, name, description, createdBy, createdDate: new Date(), status:false})
            });

            if (response.ok) {
                alert("Ticket added!");
                location.reload();
            } else {
                alert("Error adding ticket");
            }
        } catch (err) {
            console.error(err);
            alert("Connection error");
        }
    });
});
