/*!
* Start Bootstrap - Clean Blog v6.0.9 (https://startbootstrap.com/theme/clean-blog)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-clean-blog/blob/master/LICENSE)
*/

// Configuración global - usar let en lugar de const para evitar redeclaración
let baseUrl = "http://localhost:5095/api/";

window.addEventListener('DOMContentLoaded', () => {
    // Navegación sticky
    let scrollPos = 0;
    const mainNav = document.getElementById('mainNav');
    if (mainNav) {
        const headerHeight = mainNav.clientHeight;
        window.addEventListener('scroll', function () {
            const currentTop = document.body.getBoundingClientRect().top * -1;
            if (currentTop < scrollPos) {
                // Scrolling Up
                if (currentTop > 0 && mainNav.classList.contains('is-fixed')) {
                    mainNav.classList.add('is-visible');
                } else {
                    console.log(123);
                    mainNav.classList.remove('is-visible', 'is-fixed');
                }
            } else {
                // Scrolling Down
                mainNav.classList.remove(['is-visible']);
                if (currentTop > headerHeight && !mainNav.classList.contains('is-fixed')) {
                    mainNav.classList.add('is-fixed');
                }
            }
            scrollPos = currentTop;
        });
    }

    // Inicializar formulario de contacto si existe
    initializeContactForm();

    // Cargar autores si estamos en una página que los necesita
    const autoresTable = document.getElementById("bodyAutores");
    if (autoresTable) {
        loadAutores();
    }

    // Cargar libros si estamos en una página que los necesita
    const librosGrid = document.getElementById('libros-grid');
    if (librosGrid) {
        loadLibros();
    }
});

// Función para inicializar el formulario de contacto
function initializeContactForm() {
    const contactForm = document.getElementById('contactForm');
    
    if (!contactForm) return;
    
    contactForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        // Validar formulario
        if (!contactForm.checkValidity()) {
            e.stopPropagation();
            contactForm.classList.add('was-validated');
            return;
        }
        
        // Obtener datos del formulario
        const formData = {
            Nombre: document.getElementById('name').value,
            Correo: document.getElementById('email').value,
            Asunto: document.getElementById('asunto').value,
            Comentario: document.getElementById('message').value
        };
        
        // Deshabilitar botón de envío
        const submitButton = document.getElementById('submitButton');
        const originalText = submitButton.innerHTML;
        submitButton.disabled = true;
        submitButton.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Enviando...';
        
        try {
            // Enviar datos a la API
            const response = await fetch(`${baseUrl}Contact/Post-Contact`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData)
            });
            
            const result = await response.json();
            
            if (result.success) {
                // Éxito
                showNotification('success', '¡Mensaje enviado!', 'Tu mensaje ha sido enviado correctamente. Te contactaremos pronto.');
                
                // Limpiar formulario
                contactForm.reset();
                contactForm.classList.remove('was-validated');
            } else {
                // Error
                showNotification('error', 'Error', result.Message || 'Hubo un problema al enviar tu mensaje. Inténtalo de nuevo.');
            }
        } catch (error) {
            console.error('Error al enviar el formulario:', error);
            showNotification('error', 'Error de conexión', 'No se pudo conectar con el servidor. Verifica tu conexión e inténtalo de nuevo.');
        } finally {
            // Rehabilitar botón de envío
            submitButton.disabled = false;
            submitButton.innerHTML = originalText;
        }
    });
    
    // Validación en tiempo real
    const inputs = contactForm.querySelectorAll('input, textarea');
    inputs.forEach(input => {
        input.addEventListener('input', function() {
            if (this.checkValidity()) {
                this.classList.remove('is-invalid');
                this.classList.add('is-valid');
            } else {
                this.classList.remove('is-valid');
                this.classList.add('is-invalid');
            }
        });
    });
}

// Función para mostrar notificaciones (compatible si no hay SweetAlert2)
function showNotification(icon, title, text) {
    if (typeof Swal !== 'undefined') {
        Swal.fire({
            icon: icon,
            title: title,
            text: text,
            confirmButtonText: 'Aceptar'
        });
    } else {
        alert(`${title}: ${text}`);
    }
}

// Función para cargar autores
async function loadAutores() {
    const loadingState = document.getElementById('loadingState');
    const miCaja = document.getElementById("miCaja");
    
    try {
        // Mostrar estado de carga
        if (loadingState) loadingState.classList.remove('d-none');
        if (miCaja) miCaja.classList.add('d-none');
        
        const response = await fetch(baseUrl + "Autor/Get-Autores");
        const data = await response.json();
        MostrarAutores(data);
        
    } catch (e) {
        console.log(e, "Error: no se pudo conectar a la API de Autores");
        
        // Ocultar loading y mostrar error
        if (loadingState) loadingState.classList.add('d-none');
        if (miCaja) {
            miCaja.classList.remove('d-none');
        }
    }
}

async function MostrarAutores(data) {
    try {
        const bodyAutores = document.getElementById("bodyAutores");
        if (!bodyAutores) return;

        let body = '';

        data.forEach(a => {
            body +=
                `
                <tr>
                    <td>${a.nombre}</td>
                    <td>${a.apellido}</td>
                    <td>${a.telefono}</td>
                    <td>${a.pais}</td>
                    <td>${a.estado}</td>
                    <td>${a.ciudad}</td>
                </tr>
            `
        });

        bodyAutores.innerHTML = body;

    } catch (error) {
        console.log("Error al listar Autores de la API");
    }
}

// Función para cargar libros
async function loadLibros() {
    const loadingState = document.getElementById('loadingState');
    const librosGrid = document.getElementById('libros-grid');
    const miCaja = document.getElementById("miCaja");
    
    try {
        // Mostrar estado de carga
        if (loadingState) loadingState.classList.remove('d-none');
        if (librosGrid) librosGrid.classList.add('d-none');
        if (miCaja) miCaja.classList.add('d-none');
        
        const response = await fetch(baseUrl + "Libro/Get-Libros");
        const data = await response.json();
        
        if (librosGrid) {
            let html = '';
            data.forEach(libro => {
                html += crearCartaLibro(libro);
            });
            librosGrid.innerHTML = html;
            
            // Ocultar estado de carga y mostrar libros
            if (loadingState) loadingState.classList.add('d-none');
            librosGrid.classList.remove('d-none');
        }
    } catch (error) {
        console.log("Error:", error);
        
        // Ocultar estado de carga y mostrar error
        if (loadingState) loadingState.classList.add('d-none');
        if (miCaja) {
            miCaja.classList.remove('d-none');
        }
    }
}

function crearCartaLibro(libro) {
    const precio = formatearPrecio(libro.precio);
    const fecha = formatearFecha(libro.fecha_pub);
    
    return `
        <div class="col-12 col-md-6 col-lg-4 col-xl-3 d-flex justify-content-center">
            <div class="card book-card h-100">
                <div class="book-image position-relative">
                    <i class="fas fa-book fa-5x"></i>
                </div>
                <div class="card-body d-flex flex-column p-4">
                    <h5 class="card-title fw-bold fs-5 mb-3">${libro.titulo}</h5>
                    <h4 class="price-tag mb-3">${precio}</h4>
                    <p class="card-text text-muted mb-4">
                        <i class="fas fa-calendar-alt me-2"></i>
                        ${fecha}
                    </p>
                    <button class="btn btn-primary btn-lg mt-auto py-2" onclick="comprarLibro('${libro.titulo.replace(/'/g, "\\'")}')">
                        <i class="fas fa-shopping-cart me-2"></i>
                        Comprar
                    </button>
                </div>
            </div>
        </div>
    `;
}

function formatearPrecio(precio) {
    if (!precio) return 'Consultar precio';
    return new Intl.NumberFormat('es-ES', {
        style: 'currency',
        currency: 'USD'
    }).format(precio);
}

function formatearFecha(fechaISO) {
    try {
        const fecha = new Date(fechaISO);
        return fecha.toLocaleDateString('es-ES', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    } catch (error) {
        return 'Fecha no disponible';
    }
}

function comprarLibro(titulo) {
    alert(`¡Has seleccionado: "${titulo}"!`);
}