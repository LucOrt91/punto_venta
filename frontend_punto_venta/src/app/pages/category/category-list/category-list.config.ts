import { Category } from "src/app/responses/category/category.response";
import icCategory from "@iconify/icons-ic/twotone-category";
import { ListTableMenu } from "src/app/commons/list-table-menu.interface";
import icViewHeadLine from "@iconify/icons-ic/twotone-view-headline";
import icLabel from "@iconify/icons-ic/twotone-label";
import icCalendarMonth from "@iconify/icons-ic/twotone-calendar-today";
import { GenericValidators } from "@shared/validators/generic-validators";
import { TableColumns } from "src/app/core/interfaces/list-table.interfaces";

const searchOptions = [
  {
    label: "Nombre",
    value: 1,
    placeholder: "Buscar por Nombre ...",
    validation: [GenericValidators.defaultName],
    validation_desc: "Solo se permiten letras en esta busqueda",
    min_lengh: 2,
  },
  {
    label: "Descripcion",
    value: 1,
    placeholder: "Buscar por Descripcion ...",
    validation: [GenericValidators.defaultDescription],
    validation_desc: "Solo se permiten letras y numeros en esta busqueda",
    min_lengh: 2,
  },
];
const menuItems: ListTableMenu[] = [
  {
    type: "link",
    id: "all",
    icon: icViewHeadLine,
    label: "Todos",
  },
  {
    type: "link",
    id: "Activo",
    value: 1,
    icon: icLabel,
    label: "Activo",
    classes: {
      icon: "text-green",
    },
  },
  {
    type: "link",
    id: "Inactivo",
    value: 0,
    icon: icLabel,
    label: "Inactivo",
    classes: {
      icon: "text-grey",
    },
  },
];

const tableColumns: TableColumns<Category>[] = [
  {
    label: "NOMBRE",
    cssLabel: ["font-bold", "text-sm"],
    property: "name",
    cssProperty: ["font-semiblod", "text-sm", "text-left"],
    type: "text",
    sticky: true,
    sort: true,
    sortProperty: "name",
    visible: true,
    download: true,
  },
  {
    label: "DESCRIPCION",
    cssLabel: ["font-bold", "text-sm"],
    property: "description",
    cssProperty: ["font-semiblod", "text-sm", "text-left"],
    type: "text",
    sticky: false,
    sort: true,
    sortProperty: "description",
    visible: true,
    download: true,
  },
  {
    label: "F. DE CREACION",
    cssLabel: ["font-bold", "text-sm"],
    property: "auditCreateDate",
    cssProperty: ["font-semiblod", "text-sm", "text-left"],
    type: "datetime",
    sticky: false,
    sort: true,
    visible: true,
    download: true,
  },
  {
    label: "ESTADO",
    cssLabel: ["font-bold", "text-sm"],
    property: "stateCategory",
    cssProperty: ["font-semiblod", "text-sm", "text-left"],
    type: "badge",
    sticky: false,
    sort: true,
    visible: true,
    download: true,
  },
  {
    label: "",
    cssLabel: [],
    property: "icEdit",
    cssProperty: [],
    type: "icon",
    action: "edit",
    sticky: false,
    sort: false,
    visible: true,
    download: false,
  },
  {
    label: "",
    cssLabel: [],
    property: "icDelete",
    cssProperty: [],
    type: "icon",
    action: "remove",
    sticky: false,
    sort: false,
    visible: true,
    download: false,
  },
];

const filters = {
  numFilter: 0,
  textFilter: "",
  stateFilter: null,
  startDate: null,
  endDate: null,
};

const inputs = {
  numFilter: 0,
  textFilter: "",
  stateFilter: null,
  startDate: null,
  endDate: null,
};

export const ComponentSettings = {
  // ICONOS
  icCategory: icCategory,
  icCalendarMonth: icCalendarMonth,
  // LAYOUT SETTING
  menuOpen: false,
  // CONFIGURACIÓN DE LA TABLA
  tableColumns: tableColumns,
  initialSort: "Id",
  initialSortDir: "desc",
  getInputs: inputs,
  buttonLabel: "EDITAR",
  buttonLabel2: "ELIMINAR",

  // SEARCH FILTROS
  menuItems: menuItems,
  searchOptions: searchOptions,
  filters_dates_active: false,
  filters: filters,
  datesFilterArray: ["Fecha de creacion"],
  columnsFilter: tableColumns.map((column) => {
    return {
      label: column.label,
      property: column.property,
      type: column.type,
    };
  }),
};
