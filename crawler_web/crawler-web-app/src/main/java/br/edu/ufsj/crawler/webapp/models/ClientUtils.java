/**
 * ClientUtils.java
 * Copyleft 2018 - Universidade Federal de São João del-Rei
 *
 * CONTACT: jmsandy _at_ gmail _dot_ com
 */
package br.edu.ufsj.crawler.webapp.models;

import org.zkoss.zul.Row;
import org.zkoss.zul.Hbox;
import org.zkoss.zul.Label;
import org.zkoss.zul.Button;
import org.zkoss.zul.Messagebox;
import org.zkoss.zk.ui.event.Events;
import org.zkoss.zk.ui.util.Clients;
import org.zkoss.util.resource.Labels;
import org.zkoss.zk.ui.event.EventListener;
import br.edu.ufsj.crawler.webapp.viewmodels.ConfirmationResponse;

/**
 * Operações auxiliares.
 *
 * @author <a href="mailto:jmsandy@gmail.com">José Mauro da Silva Sandy</a>
 * @since 28/09/2018 20:16:24
 * @version $Id$
 */
public final class ClientUtils {

    /**
     * Mensagem exibida no centro da página.
     */
    public final static String MIDDLE_CENTER = "middle_center";
    
    /**
     * Mensagem exibida no final da página.
     */
    public final static String END_AFTER = "end_after";

    /**
     * Exibe uma mensagem de erro no cliente.
     *
     * @param message mensagem a ser exibida.
     */
    public static void showErrorNotification(String message) {
        Clients.showNotification(message, Clients.NOTIFICATION_TYPE_ERROR, null, MIDDLE_CENTER, 0);
    }

    /**
     * Exibe uma mensagem informativa no cliente.
     *
     * @param message mensagem a ser exibida.
     */
    public static void showInfoNotification(String message) {
        Clients.showNotification(message, Clients.NOTIFICATION_TYPE_INFO, null, MIDDLE_CENTER, 0);
    }

    /**
     * Exibe uma mensagem informativa no cliente.
     *
     * @param key chave de acesso no Labels {
     * @see Labels}
     * @param values valores para composição da mensagem.
     */
    public static void showInfoNotification(String key, Object... values) {
        showInfoNotification(Labels.getLabel(key, values));
    }

    /**
     * Exibe uma mensagem de confirmação para o usuário.
     *
     * @param action ação para confirmação.
     * @param model view que acionou a confirmação.
     * @param message mensagem a ser exibida.
     * @param title título da janela.
     */
    public static void showQuestionMessage(final ConfirmationResponse.ConfirmedAction action, final ConfirmationResponse model,
            String message, String title) {
        Messagebox.show(message, title,
                new Messagebox.Button[]{Messagebox.Button.YES, Messagebox.Button.NO},
                Messagebox.QUESTION, new EventListener<Messagebox.ClickEvent>() {
            @Override
            public void onEvent(Messagebox.ClickEvent e) throws Exception {
                if (model != null) {
                    switch (e.getName()) {
                        case Messagebox.ON_YES:
                            model.onConfirmClick(action, Messagebox.YES);
                            break;
                        case Messagebox.ON_NO:
                            model.onConfirmClick(action, Messagebox.NO);
                            break;
                    }
                }
            }
        });
    }

    /**
     * Adiciona o <code>text</code> como um {@link Label} dentro do {@link Row}
     * especificado.
     *
     * @param row registro para adição do label.
     * @param text texto associado ao label.
     */
    public static void appendLabel(Row row, String text) {
        row.appendChild(new Label(text));
    }

    /**
     * Appends a edit button and a remove button to the {@link Row} inside a
     * {@link Hbox} and adds the <code>ON_CLICK</code> event over the
     * {@link Row} for the edit operation.
     *
     * The edit button will call the <code>editButtonListener</code> when
     * clicked and the remove button the <code>removeButtonListener</code>.
     *
     * If <code>removeButtonListener</code> is null, it only adds the edit
     * button and the <code>ON_CLICK</code> event.
     *
     * @param row registro para adição da operações.
     * @param editButtonListener listener associado ao botão de edição..
     * @param removeButtonListener listener associado ao botão de remoção.
     * @return An array of 1 or 2 positions (depending if
     * <code>removeButtonListener</code> param is or not <code>null</code>) with
     * the edit and remove buttons. As maybe you need to disable any of them
     * depending on different situations.
     */
    public static Button[] appendOperationsAndOnClickEvent(Row row,
            EventListener editButtonListener, EventListener removeButtonListener) {
        Button[] buttons = new Button[removeButtonListener != null ? 2 : 1];

        Hbox hbox = new Hbox();
        buttons[0] = createEditButton(editButtonListener);
        hbox.appendChild(buttons[0]);

        if (removeButtonListener != null) {
            buttons[1] = createRemoveButton(removeButtonListener);
            hbox.appendChild(buttons[1]);
        }
        row.appendChild(hbox);

        row.addEventListener(Events.ON_CLICK, editButtonListener);

        return buttons;
    }

    /**
     * Creates an edit button with class and icon already set.
     *
     * @param eventListener A event listener for {@link Events.ON_CLICK}
     * @return An edit {@link Button}
     */
    public static Button createEditButton(EventListener eventListener) {
        Button result = new Button();
        result.setTooltiptext("Edit");
        result.setSclass("icono");
        result.setImage("/shared/img/ico_editar1.png");
        result.setHoverImage("/shared/img/ico_editar.png");

        result.addEventListener(Events.ON_CLICK, eventListener);

        return result;
    }

    /**
     * Creates a remove button with class and icon already set.
     *
     * @param eventListener A event listener for {@link Events.ON_CLICK}
     * @return A remove {@link Button}
     */
    public static Button createRemoveButton(EventListener eventListener) {
        Button result = new Button();
        result.setTooltiptext("Deletar");
        result.setSclass("icono");
        result.setImage("/shared/img/ico_borrar1.png");
        result.setHoverImage("/shared/img/ico_borrar.png");

        result.addEventListener(Events.ON_CLICK, eventListener);

        return result;
    }
}
