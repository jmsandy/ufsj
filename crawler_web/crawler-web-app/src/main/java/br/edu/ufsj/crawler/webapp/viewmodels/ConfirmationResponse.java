/**
 * ConfirmationResponse.java
 * Copyleft 2018 - Universidade Federal de São João del-Rei
 *
 * CONTACT: jmsandy _at_ gmail _dot_ com
 */
package br.edu.ufsj.crawler.webapp.viewmodels;

/**
 * Interface com a resposta da confirmação de uma determinada ação.
 *
 * @author <a href="mailto:jmsandy@gmail.com">José Mauro da Silva Sandy</a>
 * @since 28/09/2018 20:21:59
 * @version $Id$
 */
public interface ConfirmationResponse {

    /**
     * Enum correspondente a ação de confirmação.
     */
    enum ConfirmedAction {
        EDIT, DELETE
    }

    /**
     * Handler acionado ao confirmar a ação.
     *
     * @param action ação confirmada.
     * @param button botão precionado.
     */
    void onConfirmClick(ConfirmedAction action, int button);
}
